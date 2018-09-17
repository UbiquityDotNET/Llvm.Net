﻿// <copyright file="CodeGenerator.cs" company=".NET Foundation">
// Copyright (c) .NET Foundation. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kaleidoscope.Grammar;
using Kaleidoscope.Grammar.AST;
using Kaleidoscope.Runtime;
using Llvm.NET;
using Llvm.NET.DebugInfo;
using Llvm.NET.Instructions;
using Llvm.NET.Transforms;
using Llvm.NET.Values;

#pragma warning disable SA1512, SA1513, SA1515 // single line comments used to tag regions for extraction into docs

namespace Kaleidoscope
{
    /// <summary>Performs LLVM IR Code generation from the Kaleidoscope AST</summary>
    internal sealed class CodeGenerator
        : AstVisitorBase<Value>
        , IDisposable
        , IKaleidoscopeCodeGenerator<Value>
    {
        // <Initialization>
        public CodeGenerator( DynamicRuntimeState globalState, TargetMachine machine, string sourcePath, bool disableOptimization = false )
            : base(null)
        {
            if( globalState.LanguageLevel > LanguageLevel.MutableVariables )
            {
                throw new ArgumentException( "Language features not supported by this generator", nameof( globalState ) );
            }

            RuntimeState = globalState;
            Context = new Context( );
            TargetMachine = machine;
            DisableOptimizations = disableOptimization;
            InitializeModuleAndPassManager( sourcePath );
            InstructionBuilder = new InstructionBuilder( Context );
        }
        // </Initialization>

        public BitcodeModule Module { get; private set; }

        public void Dispose( )
        {
            Context.Dispose( );
        }

        // <Generate>
        public Value Generate( IAstNode ast )
        {
            ast.Accept( this );

            if( AnonymousFunctions.Count > 0 )
            {
                var mainFunction = Module.AddFunction( "main", Context.GetFunctionType( Context.VoidType ) );
                var block = mainFunction.AppendBasicBlock( "entry" );
                var irBuilder = new InstructionBuilder( block );
                var printdFunc = Module.AddFunction( "printd", Context.GetFunctionType( Context.DoubleType, Context.DoubleType ) );
                foreach( var anonFunc in AnonymousFunctions )
                {
                    var value = irBuilder.Call( anonFunc );
                    irBuilder.Call( printdFunc, value );
                }

                irBuilder.Return( );

                // Use always inline and Dead Code Elimination module passes to inline all of the
                // anonymous functions. This effectively strips all the calls just generated for main()
                // and inlines each of the anonymous functions directly into main, dropping the now
                // unused original anonymous functions all while retaining all of the original source
                // debug information locations.
                var mpm = new ModulePassManager( )
                          .AddAlwaysInlinerPass( )
                          .AddGlobalDCEPass( );
                mpm.Run( Module );
            }

            Module.DIBuilder.Finish( );
            return null;
        }
        // </Generate>

        // <ConstantExpression>
        public override Value Visit( Kaleidoscope.Grammar.AST.ConstantExpression constant )
        {
            return Context.CreateConstant( constant.Value );
        }
        // </ConstantExpression>

        // <BinaryOperatorExpression>
        public override Value Visit( BinaryOperatorExpression binaryOperator )
        {
            EmitLocation( binaryOperator );
            var lhs = binaryOperator.Left.Accept( this );
            var rhs = binaryOperator.Right.Accept( this );

            switch( binaryOperator.Op )
            {
            case BuiltInOperatorKind.Less:
                {
                    var tmp = InstructionBuilder.Compare( RealPredicate.UnorderedOrLessThan, lhs, rhs )
                                                .RegisterName( "cmptmp" );
                    return InstructionBuilder.UIToFPCast( tmp, InstructionBuilder.Context.DoubleType )
                                             .RegisterName( "booltmp" );
                }

            case BuiltInOperatorKind.Pow:
                {
                    var pow = GetOrDeclareFunction( new Prototype( "llvm.pow.f64", "value", "power" ) );
                    return InstructionBuilder.Call( pow, lhs, rhs )
                                             .RegisterName( "powtmp" );
                }

            case BuiltInOperatorKind.Add:
                return InstructionBuilder.FAdd( lhs, rhs ).RegisterName( "addtmp" );

            case BuiltInOperatorKind.Subtract:
                return InstructionBuilder.FSub( lhs, rhs ).RegisterName( "subtmp" );

            case BuiltInOperatorKind.Multiply:
                return InstructionBuilder.FMul( lhs, rhs ).RegisterName( "multmp" );

            case BuiltInOperatorKind.Divide:
                return InstructionBuilder.FDiv( lhs, rhs ).RegisterName( "divtmp" );

            default:
                throw new CodeGeneratorException( $"ICE: Invalid binary operator {binaryOperator.Op}" );
            }
        }
        // </BinaryOperatorExpression>

        // <FunctionCallExpression>
        public override Value Visit( FunctionCallExpression functionCall )
        {
            EmitLocation( functionCall );
            string targetName = functionCall.FunctionPrototype.Name;
            Function function;
            // try for an extern function declaration
            if( RuntimeState.FunctionDeclarations.TryGetValue( targetName, out Prototype target ) )
            {
                function = GetOrDeclareFunction( target );
            }
            else
            {
                function = Module.GetFunction( targetName ) ?? throw new CodeGeneratorException( $"Definition for function {targetName} not found" );
            }

            var args = functionCall.Arguments.Select( ctx => ctx.Accept( this ) ).ToArray( );
            EmitLocation( functionCall );
            return InstructionBuilder.Call( function, args ).RegisterName( "calltmp" );
        }
        // </FunctionCallExpression>

        // <FunctionDefinition>
        public override Value Visit( FunctionDefinition definition )
        {
            var function = GetOrDeclareFunction( definition.Signature );
            if( !function.IsDeclaration )
            {
                throw new CodeGeneratorException( $"Function {function.Name} cannot be redefined in the same module" );
            }

            LexicalBlocks.Push( function.DISubProgram );
            try
            {
                var entryBlock = function.AppendBasicBlock( "entry" );
                InstructionBuilder.PositionAtEnd( entryBlock );

                // Unset the location for the prologue emission (leading instructions with no
                // location in a function are considered part of the prologue and the debugger
                // will run past them when breaking on a function)
                EmitLocation( null );

                using( NamedValues.EnterScope( ) )
                {
                    foreach( var param in definition.Signature.Parameters )
                    {
                        var argSlot = InstructionBuilder.Alloca( function.Context.DoubleType )
                                                        .RegisterName( param.Name );
                        AddDebugInfoForAlloca( argSlot, function, param );
                        InstructionBuilder.Store( function.Parameters[ param.Index ], argSlot );
                        NamedValues[ param.Name ] = argSlot;
                    }

                    foreach( LocalVariableDeclaration local in definition.LocalVariables )
                    {
                        var localSlot = InstructionBuilder.Alloca( function.Context.DoubleType )
                                                          .RegisterName( local.Name );
                        AddDebugInfoForAlloca( localSlot, function, local );
                        NamedValues[ local.Name ] = localSlot;
                    }

                    EmitBranchToNewBlock( "body" );

                    var funcReturn = definition.Body.Accept( this );
                    InstructionBuilder.Return( funcReturn );
                    Module.DIBuilder.Finish( function.DISubProgram );
                    function.Verify( );

                    FunctionPassManager.Run( function );

                    if( definition.IsAnonymous )
                    {
                        function.AddAttribute( FunctionAttributeIndex.Function, AttributeKind.AlwaysInline )
                                .Linkage( Linkage.Private );

                        AnonymousFunctions.Add( function );
                    }

                    return function;
                }
            }
            catch( CodeGeneratorException )
            {
                function.EraseFromParent( );
                throw;
            }
        }
        // </FunctionDefinition>

        // <VariableReferenceExpression>
        public override Value Visit( VariableReferenceExpression reference )
        {
            if( !NamedValues.TryGetValue( reference.Name, out Alloca value ) )
            {
                // Source input is validated by the parser and AstBuilder, therefore
                // this is the result of an internal error in the generator rather
                // then some sort of user error.
                throw new CodeGeneratorException( $"ICE: Unknown variable name: {reference.Name}" );
            }

            EmitLocation( reference );
            return InstructionBuilder.Load( value )
                                     .RegisterName( reference.Name );
        }
        // </VariableReferenceExpression>

        // <ConditionalExpression>
        public override Value Visit( ConditionalExpression conditionalExpression )
        {
            if( !NamedValues.TryGetValue( conditionalExpression.ResultVariable.Name, out Alloca result ) )
            {
                throw new CodeGeneratorException( $"ICE: allocation for compiler generated variable '{conditionalExpression.ResultVariable.Name}' not found!" );
            }
            EmitLocation( conditionalExpression );
            var condition = conditionalExpression.Condition.Accept( this );
            if( condition == null )
            {
                return null;
            }

            EmitLocation( conditionalExpression );

            var condBool = InstructionBuilder.Compare( RealPredicate.OrderedAndNotEqual, condition, Context.CreateConstant( 0.0 ) )
                                             .RegisterName( "ifcond" );

            var function = InstructionBuilder.InsertBlock.ContainingFunction;

            var thenBlock = Context.CreateBasicBlock( "then", function );
            var elseBlock = Context.CreateBasicBlock( "else" );
            var continueBlock = Context.CreateBasicBlock( "ifcont" );
            InstructionBuilder.Branch( condBool, thenBlock, elseBlock );

            // generate then block
            InstructionBuilder.PositionAtEnd( thenBlock );
            var thenValue = conditionalExpression.ThenExpression.Accept( this );
            if( thenValue == null )
            {
                return null;
            }

            InstructionBuilder.Store( thenValue, result );
            InstructionBuilder.Branch( continueBlock );

            // capture the insert in case generating else adds new blocks
            thenBlock = InstructionBuilder.InsertBlock;

            // generate else block
            function.BasicBlocks.Add( elseBlock );
            InstructionBuilder.PositionAtEnd( elseBlock );
            var elseValue = conditionalExpression.ElseExpression.Accept( this );
            if( elseValue == null )
            {
                return null;
            }

            InstructionBuilder.Store( elseValue, result );
            InstructionBuilder.Branch( continueBlock );
            elseBlock = InstructionBuilder.InsertBlock;

            // generate continue block
            function.BasicBlocks.Add( continueBlock );
            InstructionBuilder.PositionAtEnd( continueBlock );
            return InstructionBuilder.Load( result )
                                     .RegisterName( "ifresult" );
        }
        // </ConditionalExpression>

        // <ForInExpression>
        public override Value Visit( ForInExpression forInExpression )
        {
            EmitLocation( forInExpression );
            var function = InstructionBuilder.InsertBlock.ContainingFunction;
            string varName = forInExpression.LoopVariable.Name;
            if( !NamedValues.TryGetValue( varName, out Alloca allocaVar ) )
            {
                throw new CodeGeneratorException( $"ICE: For loop initializer variable allocation not found!" );
            }

            // Emit the start code first, without 'variable' in scope.
            Value startVal = null;
            if( forInExpression.LoopVariable.Initializer != null )
            {
                startVal = forInExpression.LoopVariable.Initializer.Accept( this );
                if( startVal == null )
                {
                    return null;
                }
            }
            else
            {
                startVal = Context.CreateConstant( 0.0 );
            }

            // store the value into allocated location
            InstructionBuilder.Store( startVal, allocaVar );

            // Make the new basic block for the loop header, inserting after current
            // block.
            var preHeaderBlock = InstructionBuilder.InsertBlock;
            var loopBlock = Context.CreateBasicBlock( "loop", function );

            // Insert an explicit fall through from the current block to the loopBlock.
            InstructionBuilder.Branch( loopBlock );

            // Start insertion in loopBlock.
            InstructionBuilder.PositionAtEnd( loopBlock );

            // Within the loop, the variable is defined equal to the PHI node.
            // So, push a new scope for it and any values the body might set
            using( NamedValues.EnterScope( ) )
            {
                EmitBranchToNewBlock( "ForInScope" );

                // Emit the body of the loop.  This, like any other expression, can change the
                // current BB.  Note that we ignore the value computed by the body, but don't
                // allow an error.
                if( forInExpression.Body.Accept( this ) == null )
                {
                    return null;
                }

                Value stepValue = forInExpression.Step.Accept( this );
                if( stepValue == null )
                {
                    return null;
                }

                // Compute the end condition.
                Value endCondition = forInExpression.Condition.Accept( this );
                if( endCondition == null )
                {
                    return null;
                }

                var curVar = InstructionBuilder.Load( allocaVar )
                                               .RegisterName( varName );
                var nextVar = InstructionBuilder.FAdd( curVar, stepValue )
                                                .RegisterName( "nextvar" );
                InstructionBuilder.Store( nextVar, allocaVar );

                // Convert condition to a bool by comparing non-equal to 0.0.
                endCondition = InstructionBuilder.Compare( RealPredicate.OrderedAndNotEqual, endCondition, Context.CreateConstant( 0.0 ) )
                                                 .RegisterName( "loopcond" );

                // Create the "after loop" block and insert it.
                var loopEndBlock = InstructionBuilder.InsertBlock;
                var afterBlock = Context.CreateBasicBlock( "afterloop", function );

                // Insert the conditional branch into the end of LoopEndBB.
                InstructionBuilder.Branch( endCondition, loopBlock, afterBlock );
                InstructionBuilder.PositionAtEnd( afterBlock );

                // for expression always returns 0.0 for consistency, there is no 'void'
                return Context.DoubleType.GetNullValue( );
            }
        }
        // </ForInExpression>

        // <VarInExpression>
        public override Value Visit( VarInExpression varInExpression )
        {
            EmitLocation( varInExpression );
            using( NamedValues.EnterScope( ) )
            {
                EmitBranchToNewBlock( "VarInScope" );
                Function function = InstructionBuilder.InsertBlock.ContainingFunction;
                foreach( var localVar in varInExpression.LocalVariables )
                {
                    EmitLocation( localVar );
                    if( !NamedValues.TryGetValue( localVar.Name, out Alloca alloca ) )
                    {
                        throw new CodeGeneratorException( $"ICE: Missing allocation for local variable {localVar.Name}" );
                    }

                    Value initValue = Context.CreateConstant( 0.0 );
                    if( localVar.Initializer != null )
                    {
                        initValue = localVar.Initializer.Accept( this );
                    }

                    InstructionBuilder.Store( initValue, alloca );
                }

                EmitLocation( varInExpression );
                return varInExpression.Body.Accept( this );
            }
        }
        // </VarInExpression>

        // <AssignmentExpression>
        public override Value Visit( AssignmentExpression assignment )
        {
            var targetAlloca = NamedValues[ assignment.Target.Name ];
            var value = assignment.Value.Accept( this );
            InstructionBuilder.Store( value, targetAlloca );
            return value;
        }
        // </AssignmentExpression>

        private void EmitBranchToNewBlock( string blockName )
        {
            var newBlock = InstructionBuilder.InsertBlock.ContainingFunction.AppendBasicBlock( blockName );
            InstructionBuilder.Branch( newBlock );
            InstructionBuilder.PositionAtEnd( newBlock );
        }

        // <InitializeModuleAndPassManager>
        private void InitializeModuleAndPassManager( string sourcePath )
        {
            Module = Context.CreateBitcodeModule( Path.GetFileName( sourcePath ), SourceLanguage.C, sourcePath, "Kaleidoscope Compiler" );
            Module.TargetTriple = TargetMachine.Triple;
            Module.Layout = TargetMachine.TargetData;
            DoubleType = new DebugBasicType( Context.DoubleType, Module, "double", DiTypeKind.Float );

            FunctionPassManager = new FunctionPassManager( Module )
                                      .AddPromoteMemoryToRegisterPass( );

            if( !DisableOptimizations )
            {
                FunctionPassManager.AddInstructionCombiningPass( )
                                   .AddReassociatePass( )
                                   .AddGVNPass( )
                                   .AddCFGSimplificationPass( );
            }

            FunctionPassManager.Initialize( );
        }
        // </InitializeModuleAndPassManager>

        // <EmitLocation>
        private void EmitLocation( IAstNode node )
        {
            DIScope scope = Module.DICompileUnit;
            if( LexicalBlocks.Count > 0 )
            {
                scope = LexicalBlocks.Peek( );
            }

            InstructionBuilder.SetDebugLocation( ( uint )node.Location.StartLine, ( uint )node.Location.StartColumn, scope );
        }
        // </EmitLocation>

        // <GetOrDeclareFunction>
        // Retrieves a Function" for a prototype from the current module if it exists,
        // otherwise declares the function and returns the newly declared function.
        private Function GetOrDeclareFunction( Prototype prototype )
        {
            var function = Module.GetFunction( prototype.Name );
            if( function != null )
            {
                return function;
            }

            // extern declarations don't get debug information
            Function retVal;
            if( prototype.IsExtern )
            {
                var llvmSignature = Context.GetFunctionType( Context.DoubleType, prototype.Parameters.Select( _ => Context.DoubleType ) );
                retVal = Module.AddFunction( prototype.Name, llvmSignature );
            }
            else
            {
                var debugFile = Module.DIBuilder.CreateFile( Module.DICompileUnit.File.FileName, Module.DICompileUnit.File.Directory );
                var signature = Context.CreateFunctionType( Module.DIBuilder, DoubleType, prototype.Parameters.Select( _ => DoubleType ) );
                var lastParamLocation = prototype.Parameters.LastOrDefault( )?.Location ?? prototype.Location;

                retVal = Module.CreateFunction( Module.DICompileUnit
                                              , prototype.Name
                                              , null
                                              , debugFile
                                              , ( uint )prototype.Location.StartLine
                                              , signature
                                              , false
                                              , true
                                              , ( uint )lastParamLocation.EndLine
                                              , prototype.IsCompilerGenerated ? DebugInfoFlags.Artificial : DebugInfoFlags.Prototyped
                                              , false
                                              );
            }

            int index = 0;
            foreach( var argId in prototype.Parameters )
            {
                retVal.Parameters[ index ].Name = argId.Name;
                ++index;
            }

            return retVal;
        }
        // </GetOrDeclareFunction>

        // <AddDebugInfoForAlloca>
        private void AddDebugInfoForAlloca( Alloca argSlot, Function function, ParameterDeclaration param )
        {
            uint line = ( uint )param.Location.StartLine;
            uint col = ( uint )param.Location.StartColumn;

            DILocalVariable debugVar = Module.DIBuilder.CreateArgument( function.DISubProgram
                                                                      , param.Name
                                                                      , function.DISubProgram.File
                                                                      , line
                                                                      , DoubleType
                                                                      , true
                                                                      , DebugInfoFlags.None
                                                                      , checked(( ushort )( param.Index + 1 )) // Debug index starts at 1!
                                                                      );
            Module.DIBuilder.InsertDeclare( argSlot
                                          , debugVar
                                          , new DILocation( Context, line, col, function.DISubProgram )
                                          , InstructionBuilder.InsertBlock
                                          );
        }

        private void AddDebugInfoForAlloca( Alloca argSlot, Function function, LocalVariableDeclaration localVar )
        {
            uint line = ( uint )localVar.Location.StartLine;
            uint col = ( uint )localVar.Location.StartColumn;

            DILocalVariable debugVar = Module.DIBuilder.CreateLocalVariable( function.DISubProgram
                                                                           , localVar.Name
                                                                           , function.DISubProgram.File
                                                                           , line
                                                                           , DoubleType
                                                                           , false
                                                                           , DebugInfoFlags.None
                                                                           );
            Module.DIBuilder.InsertDeclare( argSlot
                                          , debugVar
                                          , new DILocation( Context, line, col, function.DISubProgram )
                                          , InstructionBuilder.InsertBlock
                                          );
        }
        // </AddDebugInfoForAlloca>

        // <PrivateMembers>
        private readonly DynamicRuntimeState RuntimeState;
        private readonly Context Context;
        private readonly InstructionBuilder InstructionBuilder;
        private readonly ScopeStack<Alloca> NamedValues = new ScopeStack<Alloca>( );
        private FunctionPassManager FunctionPassManager;
        private readonly bool DisableOptimizations;
        private TargetMachine TargetMachine;
        private readonly List<Function> AnonymousFunctions = new List<Function>( );
        private DebugBasicType DoubleType;
        private Stack<DIScope> LexicalBlocks = new Stack<DIScope>( );
        // </PrivateMembers>
    }
}

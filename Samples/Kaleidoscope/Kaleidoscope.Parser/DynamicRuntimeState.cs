﻿// <copyright file="DynamicRuntimeManager.cs" company=".NET Foundation">
// Copyright (c) .NET Foundation. All rights reserved.
// </copyright>

using System;
using Antlr4.Runtime;

using static Kaleidoscope.Grammar.KaleidoscopeParser;

namespace Kaleidoscope.Grammar
{
    /// <summary>Class to hold dynamic runtime global state</summary>
    /// <remarks>
    /// <para>Generally speaking the parser is the wrong place to store any
    /// sort of global state. Furthermore, the actual <see cref="KaleidoscopeParser"/>
    /// is destroyed and re-created for each REPL input string to ensure
    /// there isn't any internal parsing state carry over between parses of partial
    /// input text. Thus, this class serves to maintain state for parsing and the
    /// current state of the runtime as they are tightly linked in an interactive
    /// language like Kaleidoscope.
    /// </para>
    /// <para>
    /// This provides storage and support methods for the runtime global state.
    /// The state includes:
    /// * The language level to use for parsing
    /// * The current set of operators, including any user defined operators so the
    ///   parser knows how to resolve complex expressions with user defined operators
    ///   and precedence.
    /// * The current set of external declarations
    /// * The current set of defined functions
    /// </para>
    /// </remarks>
    public class DynamicRuntimeState
    {
        public DynamicRuntimeState( LanguageLevel languageLevel )
        {
            LanguageLevel = languageLevel;
        }

        /// <summary>Gets or sets the Language level the application supports</summary>
        public LanguageLevel LanguageLevel { get; set; }

        /// <summary>Gets a collection of function definitions parsed but not yet generated</summary>
        /// <remarks>
        /// This is a potentially dynamic set as parsing can add new entries. Also, when fully lazy
        /// compilation is implemented the definitions are removed when they are generated to native
        /// and a declaration takes it's place so that there is a sort of "Garbage Collection" to
        /// remove definitions when no longer needed.
        /// </remarks>
        public FunctionDefinitionCollection FunctionDefinitions { get; } = new FunctionDefinitionCollection( );

        /// <summary>Gets a collection of declared functions</summary>
        public PrototypeCollection FunctionDeclarations { get; } = new PrototypeCollection( );

        /// <summary>Attempts to add a new user defined operator</summary>
        /// <param name="token">Symbol for the operator</param>
        /// <param name="kind"><see cref="OperatorKind"/> value to define the behavior of the operator</param>
        /// <param name="precedence">precedence level for the operator</param>
        /// <returns><see langword="true"/> if the operator was added and <see langword="false"/> if not</returns>
        /// <remarks>
        /// This can add or replace user defined operators, however attempts to replace a built-in operator
        /// will not replace the operator and will simply return <see langword="false"/>.
        /// </remarks>
        public bool TryAddOperator( string token, OperatorKind kind, int precedence )
        {
            if( !Lexer.TokenTypeMap.TryGetValue( token, out int tokenType ) )
            {
                return false;
            }

            return TryAddOperator( tokenType, kind, precedence );
        }

        /// <summary>Attempts to add a new user defined operator</summary>
        /// <param name="token">Symbol for the operator</param>
        /// <param name="kind"><see cref="OperatorKind"/> value to define the behavior of the operator</param>
        /// <param name="precedence">precedence level for the operator</param>
        /// <returns><see langword="true"/> if the operator was added and <see langword="false"/> if not</returns>
        /// <remarks>
        /// This can add or replace user defined operators, however attempts to replace a built-in operator
        /// will not replace the operator and will simply return <see langword="false"/>.
        /// </remarks>
        public bool TryAddOperator( IToken token, OperatorKind kind, int precedence )
        {
            return TryAddOperator( token.Type, kind, precedence );
        }

        /// <summary>Gets the binary operator information for a given token type</summary>
        /// <param name="tokenType">Operator token type</param>
        /// <returns>Operator info for the operator or default if not found</returns>
        public OperatorInfo GetBinOperatorInfo( int tokenType )
        {
            if( BinOpPrecedence.TryGetValue( tokenType, out var value ) )
            {
                return value;
            }

            return default;
        }

        /// <summary>Gets the unary operator information for a given token type</summary>
        /// <param name="tokenType">Operator token type</param>
        /// <returns>Operator info for the operator or default if not found</returns>
        public OperatorInfo GetUnaryOperatorInfo( int tokenType )
        {
            if( UnaryOps.TryGetValue( tokenType, out var value ) )
            {
                return value;
            }

            return default;
        }

        /// <summary>Generates a new unique name for an anonymous function</summary>
        /// <returns>Name for an anonymous function</returns>
        public string GenerateAnonymousName( )
        {
            return $"anon_expr_{AnonymousNameIndex++}";
        }

        internal bool IsPrefixOp( int tokenType )
        {
            return UnaryOps.TryGetValue( tokenType, out var value );
        }

        internal int GetPrecedence( int tokenType ) => GetBinOperatorInfo( tokenType ).Precedence;

        internal int GetNextPrecedence( int tokenType )
        {
            var operatorInfo = GetBinOperatorInfo( tokenType );
            int retVal = operatorInfo.Precedence;
            if( operatorInfo.Kind == OperatorKind.InfixRightAssociative || operatorInfo.Kind == OperatorKind.PreFix )
            {
                return retVal;
            }

            return retVal + 1;
        }

        private bool TryAddOperator( int tokenType, OperatorKind kind, int precedence )
        {
            // internally operators are stored as token type integers to accommodate
            // simpler condition checks and switching on operator types in code generation
            switch( kind )
            {
            case OperatorKind.InfixLeftAssociative:
            case OperatorKind.InfixRightAssociative:
                return BinOpPrecedence.TryAddOrReplaceItem( new OperatorInfo( tokenType, kind, precedence, false ) );

            case OperatorKind.PreFix:
                return UnaryOps.TryAddOrReplaceItem( new OperatorInfo( tokenType, kind, 0, false ) );

            // case OperatorKind.None:
            default:
                throw new ArgumentException( "unknown kind", nameof( kind ) );
            }
        }

        private OperatorInfoCollection UnaryOps = new OperatorInfoCollection( );

        private OperatorInfoCollection BinOpPrecedence = new OperatorInfoCollection( )
        {
            new OperatorInfo( LEFTANGLE, OperatorKind.InfixLeftAssociative, 10, true),
            new OperatorInfo( PLUS,      OperatorKind.InfixLeftAssociative, 20, true),
            new OperatorInfo( MINUS,     OperatorKind.InfixLeftAssociative, 20, true),
            new OperatorInfo( ASTERISK,  OperatorKind.InfixLeftAssociative, 40, true),
            new OperatorInfo( SLASH,     OperatorKind.InfixLeftAssociative, 40, true),
            new OperatorInfo( CARET,     OperatorKind.InfixRightAssociative, 50, true),
            new OperatorInfo( ASSIGN,    OperatorKind.InfixRightAssociative, 2, true),
        };

        // this is used only to get the token type map, which is provided via a virtual
        private KaleidoscopeLexer Lexer = new KaleidoscopeLexer( null );
        private int AnonymousNameIndex;
    }
}

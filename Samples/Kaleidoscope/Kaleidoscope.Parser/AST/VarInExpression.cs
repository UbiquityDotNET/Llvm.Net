﻿// <copyright file="VarInExpression.cs" company=".NET Foundation">
// Copyright (c) .NET Foundation. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Text;

namespace Kaleidoscope.Grammar.AST
{
    public class VarInExpression
        : IExpression
    {
        public VarInExpression( SourceSpan location, IEnumerable<LocalVariableDeclaration> localVariables, IExpression body)
        {
            Location = location;
            LocalVariables = localVariables;
            Body = body;
        }

        public SourceSpan Location { get; }

        public IEnumerable<LocalVariableDeclaration> LocalVariables { get; }

        public IExpression Body { get; }

        public TResult Accept<TResult>( IAstVisitor<TResult> visitor ) => visitor.Visit( this );

        public IEnumerable<IAstNode> Children
        {
            get
            {
                foreach( var local in LocalVariables )
                {
                    yield return local;
                }

                yield return Body;
            }
        }

        public override string ToString( )
        {
            var bldr = new StringBuilder( "VarIn{" );
            foreach(var local in LocalVariables)
            {
                bldr.Append( local.ToString() );
            }

            bldr.Append( "}(" );
            bldr.Append( Body.ToString( ) );
            bldr.Append( ')' );
            return bldr.ToString( );
        }
    }
}

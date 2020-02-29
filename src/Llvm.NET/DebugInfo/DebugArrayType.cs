﻿// -----------------------------------------------------------------------
// <copyright file="DebugArrayType.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Llvm.NET.Properties;
using Llvm.NET.Types;
using Ubiquity.ArgValidators;

namespace Llvm.NET.DebugInfo
{
    /// <summary>Provides debug information binding between an <see cref="IArrayType"/>and a <see cref="DICompositeType"/></summary>
    /// <seealso href="xref:llvm_langref#dicompositetype">DICompositeType</seealso>
    public class DebugArrayType
        : DebugType<IArrayType, DICompositeType>
        , IArrayType
    {
        /// <summary>Initializes a new instance of the <see cref="DebugArrayType"/> class</summary>
        /// <param name="llvmType">Underlying LLVM array type to bind debug info to</param>
        /// <param name="elementType">Array element type with debug information</param>
        /// <param name="module">module to use for creating debug information</param>
        /// <param name="count">Number of elements in the array</param>
        /// <param name="lowerBound">Lower bound of the array [default = 0]</param>
        /// <param name="alignment">Alignment for the type</param>
        public DebugArrayType( IArrayType llvmType
                             , IDebugType<ITypeRef, DIType> elementType
                             , BitcodeModule module
                             , uint count
                             , uint lowerBound = 0
                             , uint alignment = 0
                             )
            : base( llvmType, BuildDebugType( llvmType, elementType, module, count, lowerBound, alignment ) )
        {
            DebugElementType = elementType;
        }

        /// <summary>Initializes a new instance of the <see cref="DebugArrayType"/> class.</summary>
        /// <param name="elementType">Type of elements in the array</param>
        /// <param name="module"><see cref="BitcodeModule"/> to use for the context of the debug information</param>
        /// <param name="count">Number of elements in the array</param>
        /// <param name="lowerBound"><see cref="LowerBound"/> value for the array indices [Default: 0]</param>
        public DebugArrayType( IDebugType<ITypeRef, DIType> elementType, BitcodeModule module, uint count, uint lowerBound = 0 )
            : this( elementType.ValidateNotNull( nameof( elementType ) ).CreateArrayType( count )
                  , elementType
                  , module
                  , count
                  , lowerBound
                  )
        {
        }

        /// <summary>Initializes a new instance of the <see cref="DebugArrayType"/> class.</summary>
        /// <param name="llvmType">Native LLVM type for the elements</param>
        /// <param name="module"><see cref="BitcodeModule"/> to use for the context of the debug information</param>
        /// <param name="elementType">Debug type of the array elements</param>
        /// <param name="count">Number of elements in the array</param>
        /// <param name="lowerBound"><see cref="LowerBound"/> value for the array indices [Default: 0]</param>
        public DebugArrayType( IArrayType llvmType, BitcodeModule module, DIType elementType, uint count, uint lowerBound = 0 )
            : this( DebugType.Create( llvmType.ValidateNotNull( nameof( llvmType ) ).ElementType, elementType ), module, count, lowerBound )
        {
        }

        /// <summary>Gets the full <see cref="IDebugType{NativeT, DebugT}"/> type for the elements</summary>
        public IDebugType<ITypeRef, DIType> DebugElementType { get; }

        /// <inheritdoc/>
        public ITypeRef ElementType => DebugElementType;

        /// <inheritdoc/>
        public uint Length => NativeType.Length;

        /// <summary>Gets the lower bound of the array - usually, but not always, zero</summary>
        public uint LowerBound { get; } /*=> DIType.GetOperand<DISubRange>( 0 ).LowerBound;*/

        /// <summary>Resolves a temporary metadata node for the array if full size information wasn't available at creation time</summary>
        /// <param name="layout">Type layout information</param>
        /// <param name="diBuilder">Debug information builder for creating the new debug information</param>
        public void ResolveTemporary( DataLayout layout, DebugInfoBuilder diBuilder )
        {
            if( layout == null )
            {
                throw new ArgumentNullException( nameof( layout ) );
            }

            if( diBuilder == null )
            {
                throw new ArgumentNullException( nameof( diBuilder ) );
            }

            if( DIType.IsTemporary && !DIType.IsResolved )
            {
                DIType = diBuilder.CreateArrayType( layout.BitSizeOf( NativeType )
                                                  , layout.AbiBitAlignmentOf( NativeType )
                                                  , DebugElementType.DIType
                                                  , diBuilder.CreateSubRange( LowerBound, NativeType.Length )
                                                  );
            }
        }

        private static DICompositeType BuildDebugType( [ValidatedNotNull] IArrayType llvmType
                                                     , [ValidatedNotNull] IDebugType<ITypeRef, DIType> elementType
                                                     , [ValidatedNotNull] BitcodeModule module
                                                     , uint count
                                                     , uint lowerBound
                                                     , uint alignment
                                                     )
        {
            llvmType.ValidateNotNull( nameof( llvmType ) );
            elementType.ValidateNotNull( nameof( elementType ) );
            module.ValidateNotNull( nameof( module ) );

            if( llvmType.ElementType.GetTypeRef( ) != elementType.GetTypeRef( ) )
            {
                throw new ArgumentException( Resources.ElementType_doesn_t_match_array_element_type );
            }

            if( llvmType.IsSized )
            {
                return module.DIBuilder.CreateArrayType( module.Layout.BitSizeOf( llvmType )
                                                       , alignment
                                                       , elementType.DIType
                                                       , module.DIBuilder.CreateSubRange( lowerBound, count )
                                                       );
            }

            return module.DIBuilder.CreateReplaceableCompositeType( Tag.ArrayType, string.Empty, module.DICompileUnit, null, 0 );
        }
    }
}

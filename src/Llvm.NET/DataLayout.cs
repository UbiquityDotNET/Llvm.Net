﻿// <copyright file="DataLayout.cs" company=".NET Foundation">
// Copyright (c) .NET Foundation. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Llvm.NET.Native;
using Llvm.NET.Types;
using Llvm.NET.Values;
using Ubiquity.ArgValidators;

using static Llvm.NET.Native.NativeMethods;

namespace Llvm.NET
{
    /// <summary>Provides access to LLVM target data layout information</summary>
    /// <remarks>
    /// <para>There is a distinction between various sizes and alignment for a given type
    /// that are target dependent.</para>
    /// <para>The following table illustrates the differences in sizes and their meaning
    ///  for a sample set of types.</para>
    /// |   Type  | SizeInBits | StoreSizeInBits |AllocSizeInBits|
    /// |---------|------------|-----------------|---------------|
    /// | i1      | 1          | 8               | 8             |
    /// | i8      | 8          | 8               | 8             |
    /// | i19     | 19         | 24              | 32            |
    /// | i32     | 32         | 32              | 32            |
    /// | i10     | 100        | 104             | 128           |
    /// | i128    | 128        | 128             | 128           |
    /// | Float   | 32         | 32              | 32            |
    /// | Double  | 64         | 64              | 64            |
    /// | X86_FP80| 80         | 80              | 96            |
    ///
    /// <note type="note">
    /// The allocation size depends on the alignment, and thus on the target.
    /// The values in the example table are for x86-32-linux.
    /// </note>
    /// </remarks>
    public class DataLayout
        : IDisposable
    {
        ~DataLayout( )
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose( false );
        }

        /// <inheritdoc/>
        public void Dispose( )
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>Gets the <see cref="Context"/> used for this layout (in particular, for retrieving pointer types)</summary>
        public Context Context { get; }

        /// <summary>Gets the byte ordering for this target</summary>
        public ByteOrdering Endianess => ( ByteOrdering )LLVMByteOrder( DataLayoutHandle );

        /// <summary>Gets the size of a pointer for the default address space of the target</summary>
        /// <returns>Size of a pointer to the default address space</returns>
        public uint PointerSize( ) => LLVMPointerSize( DataLayoutHandle );

        /// <summary>Retrieves the size of a pointer for a given address space of the target</summary>
        /// <param name="addressSpace">Address space for the pointer</param>
        /// <returns>Size of a pointer</returns>
        public uint PointerSize( uint addressSpace ) => LLVMPointerSizeForAS( DataLayoutHandle, addressSpace );

        /// <summary>Retrieves an LLVM integer type with the same bit width as
        /// a pointer for the default address space of the target</summary>
        /// <returns>Integer type matching the bit width of a native pointer in the target's default address space</returns>
        public ITypeRef IntPtrType( ) => TypeRef.FromHandle( LLVMIntPtrTypeInContext( Context.ContextHandle, DataLayoutHandle ) );

        /// <summary>Retrieves an LLVM integer type with the same bit width as
        /// a pointer for the given address space of the target</summary>
        /// <param name="addressSpace">Address space for the pointer</param>
        /// <returns>Integer type matching the bit width of a native pointer in the target's address space</returns>
        public ITypeRef IntPtrType( uint addressSpace )
        {
            var typeHandle = LLVMIntPtrTypeForASInContext( Context.ContextHandle, DataLayoutHandle, addressSpace );
            return TypeRef.FromHandle( typeHandle );
        }

        /// <summary>Returns the number of bits necessary to hold the specified type.</summary>
        /// <param name="typeRef">Type to retrieve the size of</param>
        /// <remarks>
        /// <para>This method determines the bit size of a type (e.g. the minimum number of
        /// bits required to represent any value of the given type.) This is distinct from the storage
        /// and stack size due to various target alignment requirements.</para>
        /// </remarks>
        /// <returns>Size of the type in bits</returns>
        public ulong BitSizeOf( ITypeRef typeRef )
        {
            VerifySized( typeRef, nameof( typeRef ) );
            return LLVMSizeOfTypeInBits( DataLayoutHandle, typeRef.GetTypeRef( ) );
        }

        /// <summary>Retrieves the number of bits required to store a value of the given type</summary>
        /// <param name="typeRef">Type to retrieve the storage size of</param>
        /// <returns>Number of bits required to store a value of the given type in the target</returns>
        /// <remarks>This method retrieves the storage size in bits of a given type. The storage size
        /// includes any trailing padding bits that may be needed if the target requires reading a wider
        /// word size. (e.g. most systems can't write a single bit value for an LLVM i1, thus the
        /// storage size is whatever the minimum number of bits that the target requires to store a value
        /// of the given type)
        /// </remarks>
        public ulong StoreSizeOf( ITypeRef typeRef )
        {
            VerifySized( typeRef, nameof( typeRef ) );
            return LLVMStoreSizeOfType( DataLayoutHandle, typeRef.GetTypeRef( ) );
        }

        /// <summary>Retrieves the ABI specified size of the given type</summary>
        /// <param name="typeRef">Type to get the size from</param>
        /// <returns>Size of the type</returns>
        public ulong AbiSizeOf( ITypeRef typeRef )
        {
            VerifySized( typeRef, nameof( typeRef ) );
            return LLVMABISizeOfType( DataLayoutHandle, typeRef.GetTypeRef( ) );
        }

        /// <summary>Retrieves the ABI specified alignment, in bytes, for a specified type</summary>
        /// <param name="typeRef">Type to get the alignment for</param>
        /// <returns>ABI specified alignment</returns>
        public uint AbiAlignmentOf( ITypeRef typeRef )
        {
            VerifySized( typeRef, nameof( typeRef ) );
            return LLVMABIAlignmentOfType( DataLayoutHandle, typeRef.GetTypeRef( ) );
        }

        /// <summary>Retrieves the call frame alignment for a given type</summary>
        /// <param name="typeRef">type to get the alignment of</param>
        /// <returns>Alignment for the type</returns>
        public uint CallFrameAlignmentOf( ITypeRef typeRef )
        {
            VerifySized( typeRef, nameof( typeRef ) );
            return LLVMCallFrameAlignmentOfType( DataLayoutHandle, typeRef.GetTypeRef( ) );
        }

        public uint PreferredAlignmentOf( ITypeRef typeRef )
        {
            VerifySized( typeRef, nameof( typeRef ) );
            return LLVMPreferredAlignmentOfType( DataLayoutHandle, typeRef.GetTypeRef( ) );
        }

        public uint PreferredAlignmentOf( Value value )
        {
            if( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            VerifySized( value.NativeType, nameof( value ) );
            return LLVMPreferredAlignmentOfGlobal( DataLayoutHandle, value.ValueHandle );
        }

        [SuppressMessage( "Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Specific type required by interop call" )]
        public uint ElementAtOffset( IStructType structType, ulong offset )
        {
            VerifySized( structType, nameof( structType ) );
            return LLVMElementAtOffset( DataLayoutHandle, structType.GetTypeRef( ), offset );
        }

        [SuppressMessage( "Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Specific type required by interop call" )]
        public ulong OffsetOfElement( IStructType structType, uint element )
        {
            VerifySized( structType, nameof( structType ) );
            return LLVMOffsetOfElement( DataLayoutHandle, structType.GetTypeRef( ), element );
        }

        public override string ToString( ) => LLVMCopyStringRepOfTargetData( DataLayoutHandle );

        public ulong ByteSizeOf( ITypeRef llvmType ) => BitSizeOf( llvmType ) / 8u;

        public uint PreferredBitAlignementOf( ITypeRef llvmType ) => PreferredAlignmentOf( llvmType ) * 8;

        public uint AbiBitAlignmentOf( ITypeRef llvmType ) => AbiAlignmentOf( llvmType ) * 8;

        public ulong BitOffsetOfElement( IStructType llvmType, uint element ) => OffsetOfElement( llvmType, element ) * 8;

        internal DataLayout( Context context, LLVMTargetDataRef targetDataHandle, bool isDisposable )
        {
            DataLayoutHandle = targetDataHandle;
            IsDisposable = isDisposable;
            Context = context;
        }

        internal static DataLayout FromHandle( Context context, LLVMTargetDataRef targetDataRef, bool isDisposable )
        {
            lock( TargetDataMap )
            {
                if( TargetDataMap.TryGetValue( targetDataRef.Pointer, out DataLayout retVal ) )
                {
                    return retVal;
                }

                // exception filter function to ensure that the native resource
                // is released on exception from adding it to the map
                retVal = new DataLayout( context, targetDataRef, isDisposable );
                bool cleanOnException( )
                {
                    retVal.Dispose( );
                    return true;
                }

                try
                {
                    TargetDataMap.Add( targetDataRef.Pointer, retVal );
                }
                catch when( cleanOnException( ) )
                {
                    // NOP
                }

                return retVal;
            }
        }

        internal LLVMTargetDataRef DataLayoutHandle { get; private set; }

        protected virtual void Dispose( bool disposing )
        {
            if( DataLayoutHandle.Pointer != IntPtr.Zero && IsDisposable )
            {
                LLVMDisposeTargetData( DataLayoutHandle );
                DataLayoutHandle = default;
            }
        }

        private static void VerifySized( [ValidatedNotNull] ITypeRef type, [InvokerParameterName] string name )
        {
            if( type == null )
            {
                throw new ArgumentNullException( name );
            }

            if( !type.IsSized )
            {
                throw new ArgumentException( "Type must be sized to get target size information", name );
            }
        }

        // indicates if this instance is disposable
        private readonly bool IsDisposable;
        private static readonly Dictionary<IntPtr, DataLayout> TargetDataMap = new Dictionary<IntPtr, DataLayout>( );
    }
}

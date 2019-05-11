// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 2.17941.31104.49410
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace Llvm.NET.Interop
{
    /// <summary>Global LLVM object handle</summary>
    [SecurityCritical]
    [GeneratedCode("LlvmBindingsGenerator","2.17941.31104.49410")]
    public class LLVMPassManagerRef
        : LlvmObjectRef
    {
        /// <summary>Creates a new instance of an LLVMPassManagerRef</summary>
        /// <param name="handle">Raw native pointer for the handle</param>
        /// <param name="owner">Value to indicate whether the handle is owned or not</param>
        public LLVMPassManagerRef( IntPtr handle, bool owner )
            : base( owner )
        {
            SetHandle( handle );
        }

        /// <summary>Gets a Zero (<see langword="null"/> value handle</summary>
        public static LLVMPassManagerRef Zero { get; } = new LLVMPassManagerRef(IntPtr.Zero, false);

        /// <inheritdoc/>
        [SecurityCritical]
        protected override bool ReleaseHandle( )
        {
            // ensure handle appears invalid from this point forward
            var prevHandle = Interlocked.Exchange( ref handle, IntPtr.Zero );
            SetHandleAsInvalid( );

            if( prevHandle != IntPtr.Zero )
            {
                LLVMDisposePassManager( handle );
            }
            return true;
        }

        private LLVMPassManagerRef( )
            : base( true )
        {
        }

        [DllImport( NativeMethods.LibraryPath, CallingConvention = CallingConvention.Cdecl )]
        private static extern void LLVMDisposePassManager( IntPtr p );
    }
}

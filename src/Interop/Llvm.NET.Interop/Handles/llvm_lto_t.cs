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

namespace Llvm.NET.Interop
{
    [SecurityCritical]
    [GeneratedCode("LlvmBindingsGenerator","2.17941.31104.49410")]
    public class llvm_lto_t
        : LlvmObjectRef
    {
        public llvm_lto_t( IntPtr handle, bool owner )
            : base( owner )
        {
            SetHandle( handle );
        }

        public static llvm_lto_t Zero { get; } = new llvm_lto_t(IntPtr.Zero, false);

        [SecurityCritical]
        protected override bool ReleaseHandle( )
        {
            llvm_destroy_optimizer( handle );
            return true;
        }

        private llvm_lto_t( )
            : base( true )
        {
        }

        [DllImport( NativeMethods.LibraryPath, CallingConvention = CallingConvention.Cdecl )]
        private static extern void llvm_destroy_optimizer( IntPtr p );
    }

    [GeneratedCode("LlvmBindingsGenerator","2.17941.31104.49410")]
    internal class llvm_lto_tAlias
        : llvm_lto_t
    {
        private llvm_lto_tAlias()
            : base( IntPtr.Zero, false )
        {
        }
    }
}

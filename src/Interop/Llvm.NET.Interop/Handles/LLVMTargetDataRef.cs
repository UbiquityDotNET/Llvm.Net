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
    public class LLVMTargetDataRef
        : LlvmObjectRef
    {
        public LLVMTargetDataRef( IntPtr handle, bool owner )
            : base( owner )
        {
            SetHandle( handle );
        }

        public static LLVMTargetDataRef Zero { get; } = new LLVMTargetDataRef(IntPtr.Zero, false);

        [SecurityCritical]
        protected override bool ReleaseHandle( )
        {
            LLVMDisposeTargetData( handle );
            return true;
        }

        private LLVMTargetDataRef( )
            : base( true )
        {
        }

        [DllImport( NativeMethods.LibraryPath, CallingConvention = CallingConvention.Cdecl )]
        private static extern void LLVMDisposeTargetData( IntPtr p );
    }

    [GeneratedCode("LlvmBindingsGenerator","2.17941.31104.49410")]
    internal class LLVMTargetDataRefAlias
        : LLVMTargetDataRef
    {
        private LLVMTargetDataRefAlias()
            : base( IntPtr.Zero, false )
        {
        }
    }
}

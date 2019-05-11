// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 2.17941.31104.49410
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Security;

namespace Llvm.NET.Interop
{
    public static partial class NativeMethods
    {
        /// <include file="BitReader.xml" path='LibLLVMAPI/Function[@name="LLVMParseBitcode"]/*' />
        [return: MarshalAs( UnmanagedType.Bool )]
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern bool LLVMParseBitcode( LLVMMemoryBufferRef MemBuf, out LLVMModuleRef OutModule, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( DisposeMessageMarshaler ) )]out string OutMessage );

        /// <include file="BitReader.xml" path='LibLLVMAPI/Function[@name="LLVMParseBitcode2"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMStatus LLVMParseBitcode2( LLVMMemoryBufferRef MemBuf, out LLVMModuleRef OutModule );

        /// <include file="BitReader.xml" path='LibLLVMAPI/Function[@name="LLVMParseBitcodeInContext2"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMStatus LLVMParseBitcodeInContext2( LLVMContextRef ContextRef, LLVMMemoryBufferRef MemBuf, out LLVMModuleRef OutModule );

        /// <include file="BitReader.xml" path='LibLLVMAPI/Function[@name="LLVMGetBitcodeModuleInContext2"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMStatus LLVMGetBitcodeModuleInContext2( LLVMContextRef ContextRef, LLVMMemoryBufferRef MemBuf, out LLVMModuleRef OutM );

        /// <include file="BitReader.xml" path='LibLLVMAPI/Function[@name="LLVMGetBitcodeModule"]/*' />
        [return: MarshalAs( UnmanagedType.Bool )]
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern bool LLVMGetBitcodeModule( LLVMMemoryBufferRef MemBuf, out LLVMModuleRef OutM, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( DisposeMessageMarshaler ) )]out string OutMessage );

        /// <include file="BitReader.xml" path='LibLLVMAPI/Function[@name="LLVMGetBitcodeModule2"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMStatus LLVMGetBitcodeModule2( LLVMMemoryBufferRef MemBuf, out LLVMModuleRef OutM );

    }
}

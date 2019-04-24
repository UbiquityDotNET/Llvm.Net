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
    [GeneratedCode("LlvmBindingsGenerator","2.17941.31104.49410")]
    public enum LLVMComdatSelectionKind : global::System.Int32
    {
        ///< The linker may choose any COMDAT.
        LLVMAnyComdatSelectionKind = 0,
        ///< The data referenced by the COMDAT must
                                             ///< be the same.
        LLVMExactMatchComdatSelectionKind = 1,
        ///< The linker will choose the largest
                                             ///< COMDAT.
        LLVMLargestComdatSelectionKind = 2,
        ///< No other Module may specify this
                                               ///< COMDAT.
        LLVMNoDuplicatesComdatSelectionKind = 3,
        ///< The data referenced by the COMDAT must be
                                          ///< the same size.
        LLVMSameSizeComdatSelectionKind = 4,
    }

    public static partial class NativeMethods
    {
        /**
         * Return the Comdat in the module with the specified name. It is created
         * if it didn't already exist.
         *
         * @see llvm::Module::getOrInsertComdat()
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMComdatRef LLVMGetOrInsertComdat( LLVMModuleRef M, [MarshalAs( UnmanagedType.LPStr )]string Name );

        /**
         * Get the Comdat assigned to the given global object.
         *
         * @see llvm::GlobalObject::getComdat()
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMComdatRef LLVMGetComdat( LLVMValueRef V );

        /**
         * Assign the Comdat to the given global object.
         *
         * @see llvm::GlobalObject::setComdat()
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMSetComdat( LLVMValueRef V, LLVMComdatRef C );

        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMComdatSelectionKind LLVMGetComdatSelectionKind( LLVMComdatRef C );

        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMSetComdatSelectionKind( LLVMComdatRef C, LLVMComdatSelectionKind Kind );

    }
}

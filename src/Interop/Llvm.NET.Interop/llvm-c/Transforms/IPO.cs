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
        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddArgumentPromotionPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddArgumentPromotionPass( LLVMPassManagerRef PM );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddConstantMergePass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddConstantMergePass( LLVMPassManagerRef PM );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddCalledValuePropagationPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddCalledValuePropagationPass( LLVMPassManagerRef PM );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddDeadArgEliminationPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddDeadArgEliminationPass( LLVMPassManagerRef PM );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddFunctionAttrsPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddFunctionAttrsPass( LLVMPassManagerRef PM );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddFunctionInliningPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddFunctionInliningPass( LLVMPassManagerRef PM );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddAlwaysInlinerPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddAlwaysInlinerPass( LLVMPassManagerRef PM );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddGlobalDCEPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddGlobalDCEPass( LLVMPassManagerRef PM );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddGlobalOptimizerPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddGlobalOptimizerPass( LLVMPassManagerRef PM );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddIPConstantPropagationPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddIPConstantPropagationPass( LLVMPassManagerRef PM );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddPruneEHPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddPruneEHPass( LLVMPassManagerRef PM );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddIPSCCPPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddIPSCCPPass( LLVMPassManagerRef PM );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddInternalizePass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddInternalizePass( LLVMPassManagerRef _0, bool AllButMain );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddStripDeadPrototypesPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddStripDeadPrototypesPass( LLVMPassManagerRef PM );

        /// <include file="IPO.xml" path='LibLLVMAPI/Function[@name="LLVMAddStripSymbolsPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddStripSymbolsPass( LLVMPassManagerRef PM );

    }
}

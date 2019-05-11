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
        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddAggressiveDCEPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddAggressiveDCEPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddBitTrackingDCEPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddBitTrackingDCEPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddAlignmentFromAssumptionsPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddAlignmentFromAssumptionsPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddCFGSimplificationPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddCFGSimplificationPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddDeadStoreEliminationPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddDeadStoreEliminationPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddScalarizerPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddScalarizerPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddMergedLoadStoreMotionPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddMergedLoadStoreMotionPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddGVNPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddGVNPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddNewGVNPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddNewGVNPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddIndVarSimplifyPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddIndVarSimplifyPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddJumpThreadingPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddJumpThreadingPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddLICMPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddLICMPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddLoopDeletionPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddLoopDeletionPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddLoopIdiomPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddLoopIdiomPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddLoopRotatePass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddLoopRotatePass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddLoopRerollPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddLoopRerollPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddLoopUnrollPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddLoopUnrollPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddLoopUnrollAndJamPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddLoopUnrollAndJamPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddLoopUnswitchPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddLoopUnswitchPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddLowerAtomicPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddLowerAtomicPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddMemCpyOptPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddMemCpyOptPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddPartiallyInlineLibCallsPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddPartiallyInlineLibCallsPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddReassociatePass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddReassociatePass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddSCCPPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddSCCPPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddScalarReplAggregatesPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddScalarReplAggregatesPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddScalarReplAggregatesPassSSA"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddScalarReplAggregatesPassSSA( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddScalarReplAggregatesPassWithThreshold"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddScalarReplAggregatesPassWithThreshold( LLVMPassManagerRef PM, int Threshold );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddSimplifyLibCallsPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddSimplifyLibCallsPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddTailCallEliminationPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddTailCallEliminationPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddConstantPropagationPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddConstantPropagationPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddDemoteMemoryToRegisterPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddDemoteMemoryToRegisterPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddVerifierPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddVerifierPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddCorrelatedValuePropagationPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddCorrelatedValuePropagationPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddEarlyCSEPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddEarlyCSEPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddEarlyCSEMemSSAPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddEarlyCSEMemSSAPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddLowerExpectIntrinsicPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddLowerExpectIntrinsicPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddTypeBasedAliasAnalysisPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddTypeBasedAliasAnalysisPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddScopedNoAliasAAPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddScopedNoAliasAAPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddBasicAliasAnalysisPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddBasicAliasAnalysisPass( LLVMPassManagerRef PM );

        /// <include file="Scalar.xml" path='LibLLVMAPI/Function[@name="LLVMAddUnifyFunctionExitNodesPass"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddUnifyFunctionExitNodesPass( LLVMPassManagerRef PM );

    }
}

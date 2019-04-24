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
    [UnmanagedFunctionPointer( global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
    public delegate System.UInt64 LLVMOrcSymbolResolverFn( [MarshalAs( UnmanagedType.LPStr )]string Name, global::System.IntPtr LookupCtx );

    [UnmanagedFunctionPointer( global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
    public delegate System.UInt64 LLVMOrcLazyCompileCallbackFn( LLVMOrcJITStackRef JITStack, global::System.IntPtr CallbackCtx );

    public static partial class NativeMethods
    {
        /**
         * Create an ORC JIT stack.
         *
         * The client owns the resulting stack, and must call OrcDisposeInstance(...)
         * to destroy it and free its memory. The JIT stack will take ownership of the
         * TargetMachine, which will be destroyed when the stack is destroyed. The
         * client should not attempt to dispose of the Target Machine, or it will result
         * in a double-free.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMOrcJITStackRef LLVMOrcCreateInstance( LLVMTargetMachineRef TM );

        /**
         * Get the error message for the most recent error (if any).
         *
         * This message is owned by the ORC JIT Stack and will be freed when the stack
         * is disposed of by LLVMOrcDisposeInstance.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        [return: MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( AliasStringMarshaler ) )]
        public static extern string LLVMOrcGetErrorMsg( LLVMOrcJITStackRef JITStack );

        /**
         * Mangle the given symbol.
         * Memory will be allocated for MangledSymbol to hold the result. The client
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMOrcGetMangledSymbol( LLVMOrcJITStackRef JITStack, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( OrcDisposeMangledSymbolMarshaler ) )]out string MangledSymbol, [MarshalAs( UnmanagedType.LPStr )]string Symbol );

        /**
         * Dispose of a mangled symbol.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMOrcDisposeMangledSymbol( out sbyte MangledSymbol );

        /**
         * Create a lazy compile callback.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMErrorRef LLVMOrcCreateLazyCompileCallback( LLVMOrcJITStackRef JITStack, out System.UInt64 RetAddr, LLVMOrcLazyCompileCallbackFn Callback, global::System.IntPtr CallbackCtx );

        /**
         * Create a named indirect call stub.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMErrorRef LLVMOrcCreateIndirectStub( LLVMOrcJITStackRef JITStack, [MarshalAs( UnmanagedType.LPStr )]string StubName, System.UInt64 InitAddr );

        /**
         * Set the pointer for the given indirect stub.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMErrorRef LLVMOrcSetIndirectStubPointer( LLVMOrcJITStackRef JITStack, [MarshalAs( UnmanagedType.LPStr )]string StubName, System.UInt64 NewAddr );

        /**
         * Add module to be eagerly compiled.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMErrorRef LLVMOrcAddEagerlyCompiledIR( LLVMOrcJITStackRef JITStack, out System.UInt64 RetHandle, LLVMModuleRef Mod, LLVMOrcSymbolResolverFn SymbolResolver, global::System.IntPtr SymbolResolverCtx );

        /**
         * Add module to be lazily compiled one function at a time.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMErrorRef LLVMOrcAddLazilyCompiledIR( LLVMOrcJITStackRef JITStack, out System.UInt64 RetHandle, LLVMModuleRef Mod, LLVMOrcSymbolResolverFn SymbolResolver, global::System.IntPtr SymbolResolverCtx );

        /**
         * Add an object file.
         *
         * This method takes ownership of the given memory buffer and attempts to add
         * it to the JIT as an object file.
         * Clients should *not* dispose of the 'Obj' argument: the JIT will manage it
         * from this call onwards.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMErrorRef LLVMOrcAddObjectFile( LLVMOrcJITStackRef JITStack, out System.UInt64 RetHandle, LLVMMemoryBufferRef Obj, LLVMOrcSymbolResolverFn SymbolResolver, global::System.IntPtr SymbolResolverCtx );

        /**
         * Remove a module set from the JIT.
         *
         * This works for all modules that can be added via OrcAdd*, including object
         * files.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMErrorRef LLVMOrcRemoveModule( LLVMOrcJITStackRef JITStack, System.UInt64 H );

        /**
         * Get symbol address from JIT instance.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMErrorRef LLVMOrcGetSymbolAddress( LLVMOrcJITStackRef JITStack, out System.UInt64 RetAddr, [MarshalAs( UnmanagedType.LPStr )]string SymbolName );

        /**
         * Get symbol address from JIT instance, searching only the specified
         * handle.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMErrorRef LLVMOrcGetSymbolAddressIn( LLVMOrcJITStackRef JITStack, out System.UInt64 RetAddr, System.UInt64 H, [MarshalAs( UnmanagedType.LPStr )]string SymbolName );

        /**
         * Register a JIT Event Listener.
         *
         * A NULL listener is ignored.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMOrcRegisterJITEventListener( LLVMOrcJITStackRef JITStack, LLVMJITEventListenerRef L );

        /**
         * Unegister a JIT Event Listener.
         *
         * A NULL listener is ignored.
         */
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMOrcUnregisterJITEventListener( LLVMOrcJITStackRef JITStack, LLVMJITEventListenerRef L );

    }
}

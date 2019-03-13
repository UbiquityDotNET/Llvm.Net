﻿// -----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company=".NET Foundation">
// Copyright (c) .NET Foundation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using Llvm.NET.Native;

using static Llvm.NET.Native.NativeMethods;

// warning CS0649: Field 'xxx' is never assigned to, and will always have its default value 0
#pragma warning disable 649

namespace Llvm.NET.JIT
{
    internal static class NativeMethods
    {
        [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
        internal delegate IntPtr LLVMMemoryManagerAllocateCodeSectionCallback( IntPtr opaque, int size, uint alignment, uint sectionID, [MarshalAs( UnmanagedType.LPStr )] string sectionName );

        [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
        internal delegate IntPtr LLVMMemoryManagerAllocateDataSectionCallback( IntPtr opaque, int size, uint alignment, uint sectionID, [MarshalAs( UnmanagedType.LPStr )] string sectionName, int isReadOnly );

        [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
        internal delegate int LLVMMemoryManagerFinalizeMemoryCallback( IntPtr opaque, out IntPtr errMsg );

        [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
        internal delegate void LLVMMemoryManagerDestroyCallback( IntPtr opaque );

        internal enum LLVMOrcErrorCode
        {
            LLVMOrcErrSuccess = 0,
            LLVMOrcErrGeneric = 1
        }

        internal struct LLVMMCJITCompilerOptions
        {
            internal uint OptLevel;
            internal LLVMCodeModel CodeModel;
            internal int NoFramePointerElim;
            internal int EnableFastISel;
            internal LLVMMCJITMemoryManagerRef MCJMM;
        }

        /*[DllImport( LibraryPath, EntryPoint = "LLVMLinkInMCJIT", CallingConvention = CallingConvention.Cdecl )]
        //internal static extern void LLVMLinkInMCJIT( );

        //[DllImport( LibraryPath, EntryPoint = "LLVMLinkInInterpreter", CallingConvention = CallingConvention.Cdecl )]
        //internal static extern void LLVMLinkInInterpreter( );
        */

        [DllImport( LibraryPath, EntryPoint = "LLVMCreateGenericValueOfInt", CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMGenericValueRef LLVMCreateGenericValueOfInt( LLVMTypeRef Ty, ulong N, [MarshalAs( UnmanagedType.Bool )]bool IsSigned );

        [DllImport( LibraryPath, EntryPoint = "LLVMCreateGenericValueOfPointer", CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMGenericValueRef LLVMCreateGenericValueOfPointer( IntPtr P );

        [DllImport( LibraryPath, EntryPoint = "LLVMCreateGenericValueOfFloat", CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMGenericValueRef LLVMCreateGenericValueOfFloat( LLVMTypeRef Ty, double N );

        [DllImport( LibraryPath, EntryPoint = "LLVMGenericValueIntWidth", CallingConvention = CallingConvention.Cdecl )]
        internal static extern uint LLVMGenericValueIntWidth( LLVMGenericValueRef GenValRef );

        [DllImport( LibraryPath, EntryPoint = "LLVMGenericValueToInt", CallingConvention = CallingConvention.Cdecl )]
        internal static extern ulong LLVMGenericValueToInt( LLVMGenericValueRef GenVal, [MarshalAs( UnmanagedType.Bool )]bool IsSigned );

        [DllImport( LibraryPath, EntryPoint = "LLVMGenericValueToPointer", CallingConvention = CallingConvention.Cdecl )]
        internal static extern IntPtr LLVMGenericValueToPointer( LLVMGenericValueRef GenVal );

        [DllImport( LibraryPath, EntryPoint = "LLVMGenericValueToFloat", CallingConvention = CallingConvention.Cdecl )]
        internal static extern double LLVMGenericValueToFloat( LLVMTypeRef TyRef, LLVMGenericValueRef GenVal );

        [DllImport( LibraryPath, EntryPoint = "LLVMCreateExecutionEngineForModule", CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMStatus LLVMCreateExecutionEngineForModule( out LLVMExecutionEngineRef OutEE, LLVMModuleRef M, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( StringMarshaler ), MarshalCookie = "DisposeMessage" )] out string OutError );

        [DllImport( LibraryPath, EntryPoint = "LLVMCreateInterpreterForModule", CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMStatus LLVMCreateInterpreterForModule( out LLVMExecutionEngineRef OutInterp, LLVMModuleRef M, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( StringMarshaler ), MarshalCookie = "DisposeMessage" )] out string OutError );

        [DllImport( LibraryPath, EntryPoint = "LLVMCreateJITCompilerForModule", CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMStatus LLVMCreateJITCompilerForModule( out LLVMExecutionEngineRef OutJIT, LLVMModuleRef M, uint OptLevel, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( StringMarshaler ), MarshalCookie = "DisposeMessage" )] out string OutError );

        [DllImport( LibraryPath, EntryPoint = "LLVMInitializeMCJITCompilerOptions", CallingConvention = CallingConvention.Cdecl )]
        internal static extern void LLVMInitializeMCJITCompilerOptions( out LLVMMCJITCompilerOptions Options, size_t SizeOfOptions );

        [DllImport( LibraryPath, EntryPoint = "LLVMCreateMCJITCompilerForModule", CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMStatus LLVMCreateMCJITCompilerForModule( out LLVMExecutionEngineRef OutJIT, LLVMModuleRef M, out LLVMMCJITCompilerOptions Options, size_t SizeOfOptions, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( StringMarshaler ), MarshalCookie = "DisposeMessage" )] out string OutError );

        [DllImport( LibraryPath, EntryPoint = "LLVMDisposeExecutionEngine", CallingConvention = CallingConvention.Cdecl )]
        internal static extern void LLVMDisposeExecutionEngine( LLVMExecutionEngineRef EE );

        [DllImport( LibraryPath, EntryPoint = "LLVMRunStaticConstructors", CallingConvention = CallingConvention.Cdecl )]
        internal static extern void LLVMRunStaticConstructors( LLVMExecutionEngineRef EE );

        [DllImport( LibraryPath, EntryPoint = "LLVMRunStaticDestructors", CallingConvention = CallingConvention.Cdecl )]
        internal static extern void LLVMRunStaticDestructors( LLVMExecutionEngineRef EE );

        [DllImport( LibraryPath, EntryPoint = "LLVMRunFunctionAsMain", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false )]
        internal static extern int LLVMRunFunctionAsMain( LLVMExecutionEngineRef EE, LLVMValueRef F, uint ArgC, string[ ] ArgV, string[ ] EnvP );

        [DllImport( LibraryPath, EntryPoint = "LLVMRunFunction", CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMGenericValueRef LLVMRunFunction( LLVMExecutionEngineRef EE, LLVMValueRef F, uint NumArgs, out LLVMGenericValueRef Args );

        /* As of LLVM 5, at least this is an empty function in the LLVM-C API
        //[DllImport( LibraryPath, EntryPoint = "LLVMFreeMachineCodeForFunction", CallingConvention = CallingConvention.Cdecl )]
        //internal static extern void LLVMFreeMachineCodeForFunction( LLVMExecutionEngineRef @EE, LLVMValueRef @F );
        */

        [DllImport( LibraryPath, EntryPoint = "LLVMAddModule", CallingConvention = CallingConvention.Cdecl )]
        internal static extern void LLVMAddModule( LLVMExecutionEngineRef EE, LLVMModuleRef M );

        [DllImport( LibraryPath, EntryPoint = "LLVMRemoveModule", CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMStatus LLVMRemoveModule( LLVMExecutionEngineRef EE, LLVMModuleRef M, out LLVMModuleRef OutMod, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( StringMarshaler ), MarshalCookie = "DisposeMessage" )] out string OutError );

        [DllImport( LibraryPath, EntryPoint = "LLVMFindFunction", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false )]
        internal static extern LLVMStatus LLVMFindFunction( LLVMExecutionEngineRef EE, [MarshalAs( UnmanagedType.LPStr )] string Name, out LLVMValueRef OutFn );

        /* As of at least LLVM 4.0.1 this just returns null
        //[DllImport( libraryPath, EntryPoint = "LLVMRecompileAndRelinkFunction", CallingConvention = CallingConvention.Cdecl )]
        //internal static extern IntPtr LLVMRecompileAndRelinkFunction( LLVMExecutionEngineRef @EE, LLVMValueRef @Fn );
        */

        [DllImport( LibraryPath, EntryPoint = "LLVMGetExecutionEngineTargetData", CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMTargetDataAlias LLVMGetExecutionEngineTargetData( LLVMExecutionEngineRef EE );

        [DllImport( LibraryPath, EntryPoint = "LLVMGetExecutionEngineTargetMachine", CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMTargetMachineAlias LLVMGetExecutionEngineTargetMachine( LLVMExecutionEngineRef EE );

        [DllImport( LibraryPath, EntryPoint = "LLVMAddGlobalMapping", CallingConvention = CallingConvention.Cdecl )]
        internal static extern void LLVMAddGlobalMapping( LLVMExecutionEngineRef EE, LLVMValueRef Global, IntPtr Addr );

        [DllImport( LibraryPath, EntryPoint = "LLVMGetPointerToGlobal", CallingConvention = CallingConvention.Cdecl )]
        internal static extern IntPtr LLVMGetPointerToGlobal( LLVMExecutionEngineRef EE, LLVMValueRef Global );

        [DllImport( LibraryPath, EntryPoint = "LLVMGetGlobalValueAddress", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false )]
        internal static extern UInt64 LLVMGetGlobalValueAddress( LLVMExecutionEngineRef EE, [MarshalAs( UnmanagedType.LPStr )] string Name );

        [DllImport( LibraryPath, EntryPoint = "LLVMGetFunctionAddress", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false )]
        internal static extern UInt64 LLVMGetFunctionAddress( LLVMExecutionEngineRef EE, [MarshalAs( UnmanagedType.LPStr )] string Name );

        [DllImport( LibraryPath, EntryPoint = "LLVMCreateSimpleMCJITMemoryManager", CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMMCJITMemoryManagerRef LLVMCreateSimpleMCJITMemoryManager( IntPtr Opaque, LLVMMemoryManagerAllocateCodeSectionCallback AllocateCodeSection, LLVMMemoryManagerAllocateDataSectionCallback AllocateDataSection, LLVMMemoryManagerFinalizeMemoryCallback FinalizeMemory, LLVMMemoryManagerDestroyCallback Destroy );

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl )]
        internal static extern void LLVMExecutionEngineClearGlobalMappingsFromModule( LLVMExecutionEngineRef ee, LLVMModuleRef m );

        /*
        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMSharedObjectBufferRef LLVMOrcMakeSharedObjectBuffer( LLVMMemoryBufferRef ObjBuffer );

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl )]
        internal static extern void LLVMOrcDisposeSharedObjectBufferRef( IntPtr SharedObjBuffer );

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMOrcErrorCode LLVMOrcAddEagerlyCompiledIR( LLVMOrcJITStackRef JITStack, out LLVMOrcModuleHandle retHandle, LLVMSharedModuleRef Mod, IntPtr SymbolResolver, IntPtr SymbolResolverCtx );

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMOrcErrorCode LLVMOrcAddObjectFile( LLVMOrcJITStackRef JITStack, out LLVMOrcModuleHandle retHandle, LLVMSharedObjectBufferRef Obj, IntPtr SymbolResolver, IntPtr SymbolResolverCtx );
        */

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMOrcJITStackRef LLVMOrcCreateInstance( LLVMTargetMachineRef TM );

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false )]
        [return: MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( StringMarshaler ) )]
        internal static extern string LLVMOrcGetErrorMsg( LLVMOrcJITStackRef JITStack );

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false )]
        internal static extern void LLVMOrcGetMangledSymbol( LLVMOrcJITStackRef JITStack
                                                          , [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( StringMarshaler ), MarshalCookie = "MangledSymbol" )] out string MangledSymbol
                                                          , [MarshalAs( UnmanagedType.LPStr )] string Symbol
                                                          );

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMOrcErrorCode LLVMOrcCreateLazyCompileCallback( LLVMOrcJITStackRef JITStack, out LLVMOrcTargetAddress retAddr, IntPtr Callback, IntPtr CallbackCtx );

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false )]
        internal static extern LLVMOrcErrorCode LLVMOrcCreateIndirectStub( LLVMOrcJITStackRef JITStack, [MarshalAs( UnmanagedType.LPStr )] string StubName, LLVMOrcTargetAddress InitAddr );

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false )]
        internal static extern LLVMOrcErrorCode LLVMOrcSetIndirectStubPointer( LLVMOrcJITStackRef JITStack, [MarshalAs( UnmanagedType.LPStr )] string StubName, LLVMOrcTargetAddress NewAddr );

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMOrcErrorCode LLVMOrcAddLazilyCompiledIR( LLVMOrcJITStackRef JITStack, out LLVMOrcModuleHandle retHandle, LLVMSharedModuleRef Mod, IntPtr SymbolResolver, IntPtr SymbolResolverCtx );

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMOrcErrorCode LLVMOrcRemoveModule( LLVMOrcJITStackRef JITStack, LLVMOrcModuleHandle H );

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false )]
        internal static extern LLVMOrcErrorCode LLVMOrcGetSymbolAddress( LLVMOrcJITStackRef JITStack, out LLVMOrcTargetAddress retAddr, [MarshalAs( UnmanagedType.LPStr )] string SymbolName );

        [DllImport( LibraryPath, CallingConvention = CallingConvention.Cdecl )]
        internal static extern LLVMOrcErrorCode LLVMOrcDisposeInstance( LLVMOrcJITStackRef JITStack );
    }
}

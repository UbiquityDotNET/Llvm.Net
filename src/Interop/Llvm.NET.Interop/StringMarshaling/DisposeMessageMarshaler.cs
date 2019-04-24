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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Llvm.NET.Interop
{
     ///<summary>Custom string marshaling class for strings using LLVMDisposeMessage</summary>
    [GeneratedCode("LlvmBindingsGenerator","2.17941.31104.49410")]
    public class DisposeMessageMarshaler
        : ICustomMarshaler
    {
        ///<Inheritdoc/>
        public void CleanUpManagedData( object ManagedObj )
        {
        }

        public void CleanUpNativeData( IntPtr pNativeData )
            => NativeDisposer?.Invoke( pNativeData );
        [SuppressMessage( "Design", "CA1024:Use properties where appropriate.", Justification = "Name and signature defined by interface")]
        public int GetNativeDataSize( ) => -1;

        public IntPtr MarshalManagedToNative( object ManagedObj )
            => throw new NotImplementedException( );

        public object MarshalNativeToManaged( IntPtr pNativeData )
            => StringNormalizer.NormalizeLineEndings( pNativeData );

        public static ICustomMarshaler GetInstance( string cookie )
        {
            switch( cookie.ToUpperInvariant( ) )
            {
            case null:
            case "":
            case "NONE":
                return new DisposeMessageMarshaler( );

            default:
                throw new ArgumentException( "Invalid marshal cookie", nameof( cookie ) );
            }
        }

        internal DisposeMessageMarshaler( )
        {
            NativeDisposer = LLVMDisposeMessage;
        }

        private readonly Action<IntPtr> NativeDisposer;

        [DllImport( NativeMethods.LibraryPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false )]
        private static extern void LLVMDisposeMessage( IntPtr p );
    }
}

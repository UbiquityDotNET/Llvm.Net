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
using System.Collections.Generic;

namespace Llvm.NET.Interop
{
    [GeneratedCode("LlvmBindingsGenerator","2.17941.31104.49410")]
    public struct LLVMTypeRef
        : IEquatable<LLVMTypeRef>
    {
        public override int GetHashCode( ) => Handle.GetHashCode( );

        public override bool Equals( object obj )
            => !( obj is null )
             && ( obj is LLVMTypeRef r )
             && ( r.Handle == Handle );

        public bool Equals( LLVMTypeRef other ) => Handle == other.Handle;

        public static bool operator ==( LLVMTypeRef lhs, LLVMTypeRef rhs )
            => EqualityComparer<LLVMTypeRef>.Default.Equals( lhs, rhs );

        public static bool operator !=( LLVMTypeRef lhs, LLVMTypeRef rhs ) => !( lhs == rhs );

        public static LLVMTypeRef Zero { get; } = new LLVMTypeRef(IntPtr.Zero);

        internal LLVMTypeRef( IntPtr p )
        {
            Handle = p;
        }

        private readonly IntPtr Handle;
    }
}

﻿// -----------------------------------------------------------------------
// <copyright file="FCmp.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Llvm.NET.Interop;

namespace Llvm.NET.Instructions
{
    /// <summary>Instruction to perform comparison of floating point values</summary>
    /// <seealso href="xref:llvm_langref#fcmp-instruction">LLVM fcmp Instruction</seealso>
    public class FCmp
        : Cmp
    {
        internal FCmp( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}

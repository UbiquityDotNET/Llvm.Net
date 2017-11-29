﻿// <copyright file="FPToSI.cs" company=".NET Foundation">
// Copyright (c) .NET Foundation. All rights reserved.
// </copyright>

using Llvm.NET.Native;

namespace Llvm.NET.Instructions
{
    /// <summary>Instruction to convert a floating point value to a signed integer type</summary>
    /// <seealso href="xref:llvm_langref#fptosi-to-instruction">LLVM fptosi .. to Instruction</seealso>
    public class FPToSI
        : Cast
    {
        internal FPToSI( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}

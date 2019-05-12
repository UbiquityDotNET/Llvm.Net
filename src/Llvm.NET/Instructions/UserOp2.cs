﻿// -----------------------------------------------------------------------
// <copyright file="UserOp2.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Llvm.NET.Interop;

namespace Llvm.NET.Instructions
{
    /// <summary>Custom operator that can be used in LLVM transform passes but should be removed before target instruction selection</summary>
    public class UserOp2 : Instruction
    {
        internal UserOp2( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}

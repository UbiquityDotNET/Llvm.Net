﻿// -----------------------------------------------------------------------
// <copyright file="ValueExtensions.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using Llvm.NET.Instructions;
using Llvm.NET.Interop;
using Llvm.NET.Properties;
using Llvm.NET.Types;

using static Llvm.NET.Interop.NativeMethods;

namespace Llvm.NET.Values
{
    /// <summary>Provides extension methods to <see cref="Value"/> that cannot be achieved as members of the class</summary>
    /// <remarks>
    /// Using generic static extension methods allows for fluent coding while retaining the type of the "this" parameter.
    /// If these were members of the <see cref="Value"/> class then the only return type could be <see cref="Value"/>,
    /// thus losing the original type and requiring a cast to get back to it.
    /// </remarks>
    public static class ValueExtensions
    {
        /// <summary>Sets the virtual register name for a value</summary>
        /// <typeparam name="T"> Type of the value to set the name for</typeparam>
        /// <param name="value">Value to set register name for</param>
        /// <param name="name">Name for the virtual register the value represents</param>
        /// <remarks>
        /// <para>Technically speaking only an <see cref="Instructions.Instruction"/> can have register name
        /// information. However, since LLVM will perform constant folding in the <see cref="Instructions.InstructionBuilder"/>
        /// most of the methods in <see cref="Instructions.InstructionBuilder"/> return a <see cref="Value"/> rather
        /// than a more specific <see cref="Instructions.Instruction"/>. Thus, without this extension method here,
        /// code would need to know ahead of time that an actual instruction would be produced then cast the result
        /// to an <see cref="Instructions.Instruction"/> and then set the debug location. This makes the code rather
        /// ugly and tedious to manage. Placing this as a generic extension method ensures that the return type matches
        /// the original and no additional casting is needed, which would defeat the purpose of doing this. For
        /// <see cref="Value"/> types that are not instructions this does nothing. This allows for a simpler fluent
        /// style of programming where the actual type is retained even in cases where an <see cref="Instructions.InstructionBuilder"/>
        /// method will always return an actual instruction.</para>
        /// <para>Since the <see cref="Value.Name"/> property is available on all <see cref="Value"/>s this is slightly
        /// redundant. It is useful for maintaining the fluent style of coding along with expressing intent more clearly.
        /// (e.g. using this makes it expressly clear that the intent is to set the virtual register name and not the
        /// name of a local variable etc...) Using the fluent style allows a significant reduction in the number of
        /// overloaded methods in <see cref="Instructions.InstructionBuilder"/> to account for all variations with or without a name.
        /// </para>
        /// </remarks>
        /// <returns><paramref name="value"/> for fluent usage</returns>
        public static T RegisterName<T>( this T value, string name )
            where T : Value
        {
            if( value is Instruction )
            {
                value.Name = name;
            }

            return value;
        }

        internal static Context GetContext( this LLVMValueRef valueRef )
        {
            if( valueRef == default )
            {
                return null;
            }

            var hType = LLVMTypeOf( valueRef );
            Debug.Assert( hType != default, Resources.Assert_Should_not_get_a_null_pointer_from_LLVM );
            var type = TypeRef.FromHandle( hType );
            return type.Context;
        }
    }
}

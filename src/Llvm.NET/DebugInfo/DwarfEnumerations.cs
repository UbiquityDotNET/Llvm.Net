﻿// <copyright file="DwarfEnumerations.cs" company=".NET Foundation">
// Copyright (c) .NET Foundation. All rights reserved.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using Llvm.NET.Interop;
using static Llvm.NET.DebugInfo.DebugInfoBuilder;

// The names describe what they are, further details are available in the DWARF specs
#pragma warning disable CS1591, SA1600, SA1602 // Enumeration items must be documented

// ReSharper disable IdentifierTypo
namespace Llvm.NET.DebugInfo
{
    /// <summary>DWARF Debug information language</summary>
    public enum SourceLanguage
    {
        /// <summary>Invalid language</summary>
        Invalid = 0,

        C89 = 0x0001,
        C = 0x0002,
        Ada83 = 0x0003,
        CPlusPlus = 0x0004,
        Cobol74 = 0x0005,
        Cobol85 = 0x0006,
        Fortran77 = 0x0007,
        Fortran90 = 0x0008,
        Pascal83 = 0x0009,
        Modula2 = 0x000a,
        Java = 0x000b,
        C99 = 0x000c,
        Ada95 = 0x000d,
        Fortran95 = 0x000e,
        PLI = 0x000f,
        ObjC = 0x0010,
        ObjCPlusPlus = 0x0011,
        UPC = 0x0012,
        D = 0x0013,

        // New in DWARF 5:
        Python = 0x0014,
        OpenCL = 0x0015,
        Go = 0x0016,
        Modula3 = 0x0017,
        Haskell = 0x0018,
        CPlusPlus03 = 0x0019,
        CPlusPlus11 = 0x001a,
        OCaml = 0x001b,

        /// <summary>Base value for unofficial languages ids</summary>
        UserMin = 0x8000,

        /// <summary>[LLVM] MIPS Assembler</summary>
        LlvmMipsAssembler = UserMin + 1,

        /// <summary>[LLVM] RenderScript</summary>
        RenderScript = UserMin + 0x0E57,

        /// <summary>[LLVM] Delphi</summary>
        Delphi = UserMin + 0x03000,

        /// <summary>[Llvm.NET] C# Language</summary>
        CSharp = UserMin + 0x01000,

        /// <summary>[Llvm.NET] .NET IL Assembly language (ILAsm)</summary>
        ILAsm = UserMin + 0x01001,

        /// <summary>Max Value for unofficial language ids</summary>
        UserMax = 0xffff
    }

    /// <summary>Tag kind for the debug information discriminated union nodes</summary>
    [SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "matches interop type from native code" )]
    public enum Tag : ushort
    {
        Invalid = 0,
        ArrayType = LLVMDwarfTag.ArrayType,
        ClassType= LLVMDwarfTag.ClassType,
        EntryPoint= LLVMDwarfTag.EntryPoint,
        EnumerationType= LLVMDwarfTag.EnumerationType,
        FormalParameter= LLVMDwarfTag.FormalParameter,
        ImportedDeclaration= LLVMDwarfTag.ImportedDeclaration,
        Label= LLVMDwarfTag.Label,
        LexicalBlock= LLVMDwarfTag.LexicalBlock,
        Member= LLVMDwarfTag.Member,
        PointerType= LLVMDwarfTag.PointerType,
        ReferenceType= LLVMDwarfTag.ReferenceType,
        CompileUnit= LLVMDwarfTag.CompileUnit,
        StringType= LLVMDwarfTag.StringType,
        StructureType= LLVMDwarfTag.StructureType,
        SubroutineType= LLVMDwarfTag.SubroutineType,
        TypeDef= LLVMDwarfTag.TypeDef,
        UnionType= LLVMDwarfTag.UnionType,
        UnspecifiedParameters= LLVMDwarfTag.UnspecifiedParameters,
        Variant= LLVMDwarfTag.Variant,
        CommonBlock= LLVMDwarfTag.CommonBlock,
        CommonInclusion= LLVMDwarfTag.CommonInclusion,
        Inheritance= LLVMDwarfTag.Inheritance,
        InlinedSubroutine= LLVMDwarfTag.InlinedSubroutine,
        Module= LLVMDwarfTag.Module,
        PtrToMemberType= LLVMDwarfTag.PtrToMemberType,
        SetType= LLVMDwarfTag.SetType,
        SubrangeType= LLVMDwarfTag.SubrangeType,
        WithStatement= LLVMDwarfTag.WithStatement,
        AccessDeclaration= LLVMDwarfTag.AccessDeclaration,
        BaseType= LLVMDwarfTag.BaseType,
        CatchBlock= LLVMDwarfTag.CatchBlock,
        ConstType= LLVMDwarfTag.ConstType,
        Constant= LLVMDwarfTag.Constant,
        Enumerator= LLVMDwarfTag.Enumerator,
        FileType= LLVMDwarfTag.FileType,
        Friend= LLVMDwarfTag.Friend,
        NameList= LLVMDwarfTag.NameList,
        NameListItem= LLVMDwarfTag.NameListItem,
        PackedType= LLVMDwarfTag.PackedType,
        SubProgram = LLVMDwarfTag.SubProgram,
        TemplateTypeParameter= LLVMDwarfTag.TemplateTypeParameter,
        TemplateValueParameter= LLVMDwarfTag.TemplateValueParameter,
        ThrownType= LLVMDwarfTag.ThrownType,
        TryBlock= LLVMDwarfTag.TryBlock,
        VariantPart= LLVMDwarfTag.VariantPart,
        Variable= LLVMDwarfTag.Variable,
        VolatileType= LLVMDwarfTag.VolatileType,
        DwarfProcedure= LLVMDwarfTag.DwarfProcedure,
        RestrictType= LLVMDwarfTag.RestrictType,
        InterfaceType= LLVMDwarfTag.InterfaceType,
        Namespace= LLVMDwarfTag.Namespace,
        ImportedModule= LLVMDwarfTag.ImportedModule,
        UnspecifiedType= LLVMDwarfTag.UnspecifiedType,
        PartialUnit= LLVMDwarfTag.PartialUnit,
        ImportedUnit= LLVMDwarfTag.ImportedUnit,
        Condition= LLVMDwarfTag.Condition,
        SharedType= LLVMDwarfTag.SharedType,
        TypeUnit= LLVMDwarfTag.TypeUnit,
        RValueReferenceType= LLVMDwarfTag.RValueReferenceType,
        TemplateAlias= LLVMDwarfTag.TemplateAlias,

        // New in DWARF 5: // New in DWARF 5:
        CoArrayType= LLVMDwarfTag.CoArrayType,
        GenericSubrange= LLVMDwarfTag.GenericSubrange,
        DynamicType= LLVMDwarfTag.DynamicType,

        // LLVM Custom constants
        AutoVariable = 0x100, // Tag for local (auto) variables.
        ArgVariable = 0x101,  // Tag for argument variables.
        Expression = 0x102,    // Tag for complex address expressions.

        UserBase = 0x1000, // Recommended base for user tags.

        MipsLoop = LLVMDwarfTag.MipsLoop,
        FormatLabel= LLVMDwarfTag.FormatLabel,
        FunctionTemplate= LLVMDwarfTag.FunctionTemplate,
        ClassTemplate= LLVMDwarfTag.ClassTemplate,
        GnuTemplateTemplateParameter = LLVMDwarfTag.GnuTemplateTemplateParam,
        GnuTemplateParameterPack= LLVMDwarfTag.GnuTemplateParameterPack,
        GnuFormalParameterPack= LLVMDwarfTag.GnuFormalParameterPack,
        LoUser= LLVMDwarfTag.LoUser,
        AppleProperty= LLVMDwarfTag.AppleProperty,
        HiUser = LLVMDwarfTag.HiUser
    }

    /// <summary>Tags for qualified types</summary>
    public enum QualifiedTypeTag
    {
        None = 0,
        Const = Tag.ConstType,
        Volatile = Tag.VolatileType
    }

    /// <summary>Primitive type supported by the debug information</summary>
    public enum DiTypeKind
    {
        Invalid = 0,

        // Encoding attribute values
        Address = 0x01,
        Boolean = 0x02,
        ComplexFloat = 0x03,
        Float = 0x04,
        Signed = 0x05,
        SignedChar = 0x06,
        Unsigned = 0x07,
        UnsignedChar = 0x08,
        ImaginaryFloat = 0x09,
        PackedDecimal = 0x0a,
        NumericString = 0x0b,
        Edited = 0x0c,
        SignedFixed = 0x0d,
        UnsignedFixed = 0x0e,
        DecimalFloat = 0x0f,
        UTF = 0x10,
        LoUser = 0x80,
        HiUser = 0xff
    }

    /// <summary>Accessibility flags</summary>
    /// <remarks>
    /// The three accessibility flags are mutually exclusive and rolled together
    /// in the first two bits.
    /// </remarks>
    [SuppressMessage( "Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags", Justification = "Matches the underlying wrapped API" )]
    [Flags]
    public enum DebugInfoFlags
    {
        None = 0,
        Private = 1,
        Protected = 2,
        Public = 3,
        AccessibilityMask = ( 1 << 0 ) | ( 1 << 1 ),

        ForwardDeclaration = 1 << 2,
        AppleBlock = 1 << 3,
        BlockByrefStruct = 1 << 4,
        Virtual = 1 << 5,
        Artificial = 1 << 6,
        Explicit = 1 << 7,
        Prototyped = 1 << 8,
        ObjcClassComplete = 1 << 9,
        ObjectPointer = 1 << 10,
        Vector = 1 << 11,
        StaticMember = 1 << 12,
        LValueReference = 1 << 13,
        RValueReference = 1 << 14,
        /* Reserved = 1 << 15,*/
        SingleInheritance = 1 << 16,
        MultipleInheritance = 2 << 16,
        VirtualInheritance = 3 << 16,
        IntroducedVirtual = 1 << 18,
        BitField = 1 << 19,
        NoReturn = 1 << 20,
        MainSubprogram = 1 << 21,
        IndirectVirtualBase = ForwardDeclaration | Virtual
    }

#pragma warning disable SA1300 // Element must begin with upper-case letter
    /// <summary>Debug information expression operator</summary>
    [SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Matches underlying interop type" )]
    public enum ExpressionOp : long
    {
        Invalid = 0,
        addr = 0x03,
        deref = 0x06,
        const1u = 0x08,
        const1s = 0x09,
        const2u = 0x0a,
        const2s = 0x0b,
        const4u = 0x0c,
        const4s = 0x0d,
        const8u = 0x0e,
        const8s = 0x0f,
        constu = 0x10,
        consts = 0x11,
        dup = 0x12,
        drop = 0x13,
        over = 0x14,
        pick = 0x15,
        swap = 0x16,
        rot = 0x17,
        xderef = 0x18,
        abs = 0x19,
        and = 0x1a,
        div = 0x1b,
        minus = 0x1c,
        mod = 0x1d,
        mul = 0x1e,
        neg = 0x1f,
        not = 0x20,
        or = 0x21,
        plus = 0x22,
        plus_uconst = 0x23,
        shl = 0x24,
        shr = 0x25,
        shra = 0x26,
        xor = 0x27,
        skip = 0x2f,
        bra = 0x28,
        eq = 0x29,
        ge = 0x2a,
        gt = 0x2b,
        le = 0x2c,
        lt = 0x2d,
        ne = 0x2e,
        lit0 = 0x30,
        lit1 = 0x31,
        lit2 = 0x32,
        lit3 = 0x33,
        lit4 = 0x34,
        lit5 = 0x35,
        lit6 = 0x36,
        lit7 = 0x37,
        lit8 = 0x38,
        lit9 = 0x39,
        lit10 = 0x3a,
        lit11 = 0x3b,
        lit12 = 0x3c,
        lit13 = 0x3d,
        lit14 = 0x3e,
        lit15 = 0x3f,
        lit16 = 0x40,
        lit17 = 0x41,
        lit18 = 0x42,
        lit19 = 0x43,
        lit20 = 0x44,
        lit21 = 0x45,
        lit22 = 0x46,
        lit23 = 0x47,
        lit24 = 0x48,
        lit25 = 0x49,
        lit26 = 0x4a,
        lit27 = 0x4b,
        lit28 = 0x4c,
        lit29 = 0x4d,
        lit30 = 0x4e,
        lit31 = 0x4f,
        reg0 = 0x50,
        reg1 = 0x51,
        reg2 = 0x52,
        reg3 = 0x53,
        reg4 = 0x54,
        reg5 = 0x55,
        reg6 = 0x56,
        reg7 = 0x57,
        reg8 = 0x58,
        reg9 = 0x59,
        reg10 = 0x5a,
        reg11 = 0x5b,
        reg12 = 0x5c,
        reg13 = 0x5d,
        reg14 = 0x5e,
        reg15 = 0x5f,
        reg16 = 0x60,
        reg17 = 0x61,
        reg18 = 0x62,
        reg19 = 0x63,
        reg20 = 0x64,
        reg21 = 0x65,
        reg22 = 0x66,
        reg23 = 0x67,
        reg24 = 0x68,
        reg25 = 0x69,
        reg26 = 0x6a,
        reg27 = 0x6b,
        reg28 = 0x6c,
        reg29 = 0x6d,
        reg30 = 0x6e,
        reg31 = 0x6f,
        breg0 = 0x70,
        breg1 = 0x71,
        breg2 = 0x72,
        breg3 = 0x73,
        breg4 = 0x74,
        breg5 = 0x75,
        breg6 = 0x76,
        breg7 = 0x77,
        breg8 = 0x78,
        breg9 = 0x79,
        breg10 = 0x7a,
        breg11 = 0x7b,
        breg12 = 0x7c,
        breg13 = 0x7d,
        breg14 = 0x7e,
        breg15 = 0x7f,
        breg16 = 0x80,
        breg17 = 0x81,
        breg18 = 0x82,
        breg19 = 0x83,
        breg20 = 0x84,
        breg21 = 0x85,
        breg22 = 0x86,
        breg23 = 0x87,
        breg24 = 0x88,
        breg25 = 0x89,
        breg26 = 0x8a,
        breg27 = 0x8b,
        breg28 = 0x8c,
        breg29 = 0x8d,
        breg30 = 0x8e,
        breg31 = 0x8f,
        regx = 0x90,
        fbreg = 0x91,
        bregx = 0x92,
        piece = 0x93,
        deref_size = 0x94,
        xderef_size = 0x95,
        nop = 0x96,
        push_object_address = 0x97,
        call2 = 0x98,
        call4 = 0x99,
        call_ref = 0x9a,
        form_tls_address = 0x9b,
        call_frame_cfa = 0x9c,
        bit_piece = 0x9d,
        implicit_value = 0x9e,
        stack_value = 0x9f,

        // Extensions for GNU-style thread-local storage.
        GNU_push_tls_address = 0xe0,

        // Extensions for Fission proposal.
        GNU_addr_index = 0xfb,
        GNU_const_index = 0xfc
    }
#pragma warning restore SA1300 // Element must begin with upper-case letter
}

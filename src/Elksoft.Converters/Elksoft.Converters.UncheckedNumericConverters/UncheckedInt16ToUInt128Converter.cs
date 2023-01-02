﻿// <copyright file="UncheckedInt16ToUInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt16ToUInt128Converter : Converter<Int16, UInt128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt128 Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt128)value);
        }
    }
}
#endif

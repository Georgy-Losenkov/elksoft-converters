﻿// <copyright file="CheckedUInt128ToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt128ToInt64Converter : Converter<UInt128, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int64 Convert(UInt128 value, IFormatProvider? formatProvider)
        {
            return checked((Int64)value);
        }
    }
}
#endif

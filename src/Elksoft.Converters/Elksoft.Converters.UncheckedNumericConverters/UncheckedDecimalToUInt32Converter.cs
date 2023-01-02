﻿// <copyright file="UncheckedDecimalToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedDecimalToUInt32Converter : Converter<Decimal, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt32 Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt32)value);
        }
    }
}
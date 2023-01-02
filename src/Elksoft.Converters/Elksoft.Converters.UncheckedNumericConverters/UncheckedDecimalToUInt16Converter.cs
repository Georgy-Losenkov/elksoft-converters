// <copyright file="UncheckedDecimalToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedDecimalToUInt16Converter : Converter<Decimal, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt16)value);
        }
    }
}

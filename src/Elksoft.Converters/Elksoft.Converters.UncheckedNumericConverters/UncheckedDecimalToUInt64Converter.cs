// <copyright file="UncheckedDecimalToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedDecimalToUInt64Converter : Converter<Decimal, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt64 Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt64)value);
        }
    }
}

// <copyright file="UncheckedDecimalToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedDecimalToInt64Converter : Converter<Decimal, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int64 Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return unchecked((Int64)value);
        }
    }
}

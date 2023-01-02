// <copyright file="CheckedDecimalToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedDecimalToInt64Converter : Converter<Decimal, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int64 Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return checked((Int64)value);
        }
    }
}

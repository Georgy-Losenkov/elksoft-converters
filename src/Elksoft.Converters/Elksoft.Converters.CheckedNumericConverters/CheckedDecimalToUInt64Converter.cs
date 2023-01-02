// <copyright file="CheckedDecimalToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedDecimalToUInt64Converter : Converter<Decimal, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt64 Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return checked((UInt64)value);
        }
    }
}

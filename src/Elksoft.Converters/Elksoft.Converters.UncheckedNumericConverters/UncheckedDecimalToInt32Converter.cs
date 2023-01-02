// <copyright file="UncheckedDecimalToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedDecimalToInt32Converter : Converter<Decimal, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int32 Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return unchecked((Int32)value);
        }
    }
}

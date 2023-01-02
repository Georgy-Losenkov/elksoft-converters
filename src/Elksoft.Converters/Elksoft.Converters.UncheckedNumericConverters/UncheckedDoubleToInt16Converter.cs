// <copyright file="UncheckedDoubleToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedDoubleToInt16Converter : Converter<Double, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int16 Convert(Double value, IFormatProvider? formatProvider)
        {
            return unchecked((Int16)value);
        }
    }
}

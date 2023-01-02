// <copyright file="UncheckedDoubleToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedDoubleToUInt16Converter : Converter<Double, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(Double value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt16)value);
        }
    }
}

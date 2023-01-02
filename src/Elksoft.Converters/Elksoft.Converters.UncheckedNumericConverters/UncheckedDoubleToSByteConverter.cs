// <copyright file="UncheckedDoubleToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedDoubleToSByteConverter : Converter<Double, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Double value, IFormatProvider? formatProvider)
        {
            return unchecked((SByte)value);
        }
    }
}

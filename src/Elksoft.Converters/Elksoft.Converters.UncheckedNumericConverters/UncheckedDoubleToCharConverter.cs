// <copyright file="UncheckedDoubleToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedDoubleToCharConverter : Converter<Double, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(Double value, IFormatProvider? formatProvider)
        {
            return unchecked((Char)value);
        }
    }
}

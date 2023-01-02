// <copyright file="CheckedDoubleToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedDoubleToInt16Converter : Converter<Double, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int16 Convert(Double value, IFormatProvider? formatProvider)
        {
            return checked((Int16)value);
        }
    }
}

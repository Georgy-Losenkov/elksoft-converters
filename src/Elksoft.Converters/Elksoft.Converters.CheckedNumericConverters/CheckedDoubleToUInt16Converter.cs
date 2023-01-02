// <copyright file="CheckedDoubleToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedDoubleToUInt16Converter : Converter<Double, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(Double value, IFormatProvider? formatProvider)
        {
            return checked((UInt16)value);
        }
    }
}

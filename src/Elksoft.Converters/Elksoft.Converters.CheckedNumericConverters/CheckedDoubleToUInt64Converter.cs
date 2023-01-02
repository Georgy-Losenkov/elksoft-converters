// <copyright file="CheckedDoubleToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedDoubleToUInt64Converter : Converter<Double, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt64 Convert(Double value, IFormatProvider? formatProvider)
        {
            return checked((UInt64)value);
        }
    }
}

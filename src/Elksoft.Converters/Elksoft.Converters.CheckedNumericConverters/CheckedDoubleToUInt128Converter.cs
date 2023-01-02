// <copyright file="CheckedDoubleToUInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedDoubleToUInt128Converter : Converter<Double, UInt128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt128 Convert(Double value, IFormatProvider? formatProvider)
        {
            return checked((UInt128)value);
        }
    }
}
#endif

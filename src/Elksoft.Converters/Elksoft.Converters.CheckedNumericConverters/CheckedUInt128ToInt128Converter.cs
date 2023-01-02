// <copyright file="CheckedUInt128ToInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt128ToInt128Converter : Converter<UInt128, Int128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int128 Convert(UInt128 value, IFormatProvider? formatProvider)
        {
            return checked((Int128)value);
        }
    }
}
#endif

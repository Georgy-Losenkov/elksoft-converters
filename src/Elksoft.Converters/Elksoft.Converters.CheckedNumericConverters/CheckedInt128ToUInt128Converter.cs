// <copyright file="CheckedInt128ToUInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt128ToUInt128Converter : Converter<Int128, UInt128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt128 Convert(Int128 value, IFormatProvider? formatProvider)
        {
            return checked((UInt128)value);
        }
    }
}
#endif

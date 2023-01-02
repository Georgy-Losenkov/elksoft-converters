// <copyright file="CheckedUInt128ToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt128ToUInt16Converter : Converter<UInt128, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(UInt128 value, IFormatProvider? formatProvider)
        {
            return checked((UInt16)value);
        }
    }
}
#endif

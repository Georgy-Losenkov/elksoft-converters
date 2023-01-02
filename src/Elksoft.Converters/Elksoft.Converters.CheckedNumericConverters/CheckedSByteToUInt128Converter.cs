// <copyright file="CheckedSByteToUInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedSByteToUInt128Converter : Converter<SByte, UInt128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt128 Convert(SByte value, IFormatProvider? formatProvider)
        {
            return checked((UInt128)value);
        }
    }
}
#endif

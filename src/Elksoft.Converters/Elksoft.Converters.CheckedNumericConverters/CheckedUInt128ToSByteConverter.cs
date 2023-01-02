// <copyright file="CheckedUInt128ToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt128ToSByteConverter : Converter<UInt128, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(UInt128 value, IFormatProvider? formatProvider)
        {
            return checked((SByte)value);
        }
    }
}
#endif

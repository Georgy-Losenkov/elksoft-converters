// <copyright file="CheckedSByteToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedSByteToUInt16Converter : Converter<SByte, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(SByte value, IFormatProvider? formatProvider)
        {
            return checked((UInt16)value);
        }
    }
}

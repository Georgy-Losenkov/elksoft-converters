// <copyright file="CheckedUInt16ToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt16ToSByteConverter : Converter<UInt16, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(UInt16 value, IFormatProvider? formatProvider)
        {
            return checked((SByte)value);
        }
    }
}

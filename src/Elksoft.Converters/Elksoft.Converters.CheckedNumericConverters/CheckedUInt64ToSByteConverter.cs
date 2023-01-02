// <copyright file="CheckedUInt64ToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt64ToSByteConverter : Converter<UInt64, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(UInt64 value, IFormatProvider? formatProvider)
        {
            return checked((SByte)value);
        }
    }
}

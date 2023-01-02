// <copyright file="CheckedUInt32ToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt32ToSByteConverter : Converter<UInt32, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return checked((SByte)value);
        }
    }
}

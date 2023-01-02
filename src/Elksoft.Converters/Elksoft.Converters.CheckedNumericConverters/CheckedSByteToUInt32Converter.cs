// <copyright file="CheckedSByteToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedSByteToUInt32Converter : Converter<SByte, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt32 Convert(SByte value, IFormatProvider? formatProvider)
        {
            return checked((UInt32)value);
        }
    }
}

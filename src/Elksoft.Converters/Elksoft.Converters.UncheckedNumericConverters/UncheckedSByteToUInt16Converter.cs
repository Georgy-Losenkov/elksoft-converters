// <copyright file="UncheckedSByteToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedSByteToUInt16Converter : Converter<SByte, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(SByte value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt16)value);
        }
    }
}

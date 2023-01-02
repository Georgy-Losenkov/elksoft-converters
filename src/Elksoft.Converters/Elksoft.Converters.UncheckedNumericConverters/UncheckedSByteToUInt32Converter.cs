// <copyright file="UncheckedSByteToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedSByteToUInt32Converter : Converter<SByte, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt32 Convert(SByte value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt32)value);
        }
    }
}

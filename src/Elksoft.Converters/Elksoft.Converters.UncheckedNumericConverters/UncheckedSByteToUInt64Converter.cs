// <copyright file="UncheckedSByteToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedSByteToUInt64Converter : Converter<SByte, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt64 Convert(SByte value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt64)value);
        }
    }
}

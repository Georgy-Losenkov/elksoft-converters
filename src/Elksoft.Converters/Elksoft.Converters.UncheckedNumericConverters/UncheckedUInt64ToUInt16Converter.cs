// <copyright file="UncheckedUInt64ToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedUInt64ToUInt16Converter : Converter<UInt64, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(UInt64 value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt16)value);
        }
    }
}

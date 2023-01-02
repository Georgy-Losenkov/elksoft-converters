// <copyright file="UncheckedUInt64ToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedUInt64ToInt64Converter : Converter<UInt64, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int64 Convert(UInt64 value, IFormatProvider? formatProvider)
        {
            return unchecked((Int64)value);
        }
    }
}

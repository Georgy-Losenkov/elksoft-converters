// <copyright file="UncheckedInt64ToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt64ToUInt32Converter : Converter<Int64, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt32 Convert(Int64 value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt32)value);
        }
    }
}

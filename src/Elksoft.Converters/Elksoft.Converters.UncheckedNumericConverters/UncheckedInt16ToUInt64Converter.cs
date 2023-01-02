// <copyright file="UncheckedInt16ToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt16ToUInt64Converter : Converter<Int16, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt64 Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt64)value);
        }
    }
}

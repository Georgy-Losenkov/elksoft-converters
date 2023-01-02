// <copyright file="UncheckedUInt32ToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedUInt32ToInt32Converter : Converter<UInt32, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int32 Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Int32)value);
        }
    }
}

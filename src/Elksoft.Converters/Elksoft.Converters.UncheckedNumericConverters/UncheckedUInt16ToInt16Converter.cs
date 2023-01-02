// <copyright file="UncheckedUInt16ToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedUInt16ToInt16Converter : Converter<UInt16, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int16 Convert(UInt16 value, IFormatProvider? formatProvider)
        {
            return unchecked((Int16)value);
        }
    }
}

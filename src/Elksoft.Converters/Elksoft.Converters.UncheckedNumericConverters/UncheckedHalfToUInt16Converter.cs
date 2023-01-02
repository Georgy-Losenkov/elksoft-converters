// <copyright file="UncheckedHalfToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedHalfToUInt16Converter : Converter<Half, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(Half value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt16)value);
        }
    }
}
#endif

// <copyright file="UncheckedHalfToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedHalfToInt16Converter : Converter<Half, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int16 Convert(Half value, IFormatProvider? formatProvider)
        {
            return unchecked((Int16)value);
        }
    }
}
#endif

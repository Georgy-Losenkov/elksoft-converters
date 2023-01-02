// <copyright file="CheckedHalfToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedHalfToInt16Converter : Converter<Half, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int16 Convert(Half value, IFormatProvider? formatProvider)
        {
            return checked((Int16)value);
        }
    }
}
#endif

// <copyright file="UncheckedHalfToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedHalfToSByteConverter : Converter<Half, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Half value, IFormatProvider? formatProvider)
        {
            return unchecked((SByte)value);
        }
    }
}
#endif

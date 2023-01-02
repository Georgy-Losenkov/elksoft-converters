// <copyright file="UncheckedHalfToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedHalfToCharConverter : Converter<Half, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(Half value, IFormatProvider? formatProvider)
        {
            return unchecked((Char)value);
        }
    }
}
#endif

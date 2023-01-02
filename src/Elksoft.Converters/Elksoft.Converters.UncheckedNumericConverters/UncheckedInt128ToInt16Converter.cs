// <copyright file="UncheckedInt128ToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt128ToInt16Converter : Converter<Int128, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int16 Convert(Int128 value, IFormatProvider? formatProvider)
        {
            return unchecked((Int16)value);
        }
    }
}
#endif

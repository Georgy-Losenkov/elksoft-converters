// <copyright file="UncheckedInt128ToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt128ToInt32Converter : Converter<Int128, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int32 Convert(Int128 value, IFormatProvider? formatProvider)
        {
            return unchecked((Int32)value);
        }
    }
}
#endif

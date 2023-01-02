// <copyright file="UncheckedInt128ToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt128ToInt64Converter : Converter<Int128, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int64 Convert(Int128 value, IFormatProvider? formatProvider)
        {
            return unchecked((Int64)value);
        }
    }
}
#endif

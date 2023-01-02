// <copyright file="UncheckedInt128ToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt128ToDecimalConverter : Converter<Int128, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Decimal Convert(Int128 value, IFormatProvider? formatProvider)
        {
            return unchecked((Decimal)value);
        }
    }
}
#endif

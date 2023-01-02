// <copyright file="CheckedUInt128ToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt128ToDecimalConverter : Converter<UInt128, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Decimal Convert(UInt128 value, IFormatProvider? formatProvider)
        {
            return checked((Decimal)value);
        }
    }
}
#endif

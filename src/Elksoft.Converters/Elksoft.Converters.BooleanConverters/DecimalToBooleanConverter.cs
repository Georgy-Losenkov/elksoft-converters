// <copyright file="DecimalToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class DecimalToBooleanConverter : Converter<Decimal, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return value != Decimal.Zero;
        }
    }
}

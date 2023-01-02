// <copyright file="CheckedDoubleToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedDoubleToDecimalConverter : Converter<Double, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Decimal Convert(Double value, IFormatProvider? formatProvider)
        {
            return checked((Decimal)value);
        }
    }
}

// <copyright file="BooleanToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToDecimalConverter : Converter<Boolean, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Decimal Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? Decimal.One : Decimal.Zero;
        }
    }
}

// <copyright file="CheckedSingleToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedSingleToDecimalConverter : Converter<Single, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Decimal Convert(Single value, IFormatProvider? formatProvider)
        {
            return checked((Decimal)value);
        }
    }
}

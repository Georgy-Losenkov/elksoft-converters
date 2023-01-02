// <copyright file="UncheckedSingleToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedSingleToDecimalConverter : Converter<Single, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Decimal Convert(Single value, IFormatProvider? formatProvider)
        {
            return unchecked((Decimal)value);
        }
    }
}

// <copyright file="StringToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToDecimalConverter : Converter<String, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Decimal Convert(String? value, IFormatProvider? formatProvider)
        {
            return Decimal.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

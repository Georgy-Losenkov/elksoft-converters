// <copyright file="StringToDoubleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToDoubleConverter : Converter<String, Double>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Double Convert(String? value, IFormatProvider? formatProvider)
        {
            return Double.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

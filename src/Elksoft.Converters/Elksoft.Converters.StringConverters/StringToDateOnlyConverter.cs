// <copyright file="StringToDateOnlyConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToDateOnlyConverter : Converter<String, DateOnly>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override DateOnly Convert(String? value, IFormatProvider? formatProvider)
        {
            return DateOnly.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

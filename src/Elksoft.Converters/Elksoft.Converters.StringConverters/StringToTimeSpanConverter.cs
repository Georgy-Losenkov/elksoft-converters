// <copyright file="StringToTimeSpanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToTimeSpanConverter : Converter<String, TimeSpan>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override TimeSpan Convert(String? value, IFormatProvider? formatProvider)
        {
            return TimeSpan.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

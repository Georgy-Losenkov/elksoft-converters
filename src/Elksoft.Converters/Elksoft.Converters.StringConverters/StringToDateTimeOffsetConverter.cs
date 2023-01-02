// <copyright file="StringToDateTimeOffsetConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToDateTimeOffsetConverter : Converter<String, DateTimeOffset>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override DateTimeOffset Convert(String? value, IFormatProvider? formatProvider)
        {
            return DateTimeOffset.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

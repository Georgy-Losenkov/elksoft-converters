// <copyright file="StringToDateTimeConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToDateTimeConverter : Converter<String, DateTime>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override DateTime Convert(String? value, IFormatProvider? formatProvider)
        {
            return DateTime.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

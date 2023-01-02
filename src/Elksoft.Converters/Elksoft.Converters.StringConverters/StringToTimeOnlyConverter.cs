// <copyright file="StringToTimeOnlyConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToTimeOnlyConverter : Converter<String, TimeOnly>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override TimeOnly Convert(String? value, IFormatProvider? formatProvider)
        {
            return TimeOnly.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

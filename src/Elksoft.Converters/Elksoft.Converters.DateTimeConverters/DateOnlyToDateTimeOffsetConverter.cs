// <copyright file="DateOnlyToDateTimeOffsetConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class DateOnlyToDateTimeOffsetConverter : Converter<DateOnly, DateTimeOffset>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override DateTimeOffset Convert(DateOnly value, IFormatProvider? formatProvider)
        {
            return value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Unspecified);
        }
    }
}
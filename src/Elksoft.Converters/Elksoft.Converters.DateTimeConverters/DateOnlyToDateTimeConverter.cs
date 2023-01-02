// <copyright file="DateOnlyToDateTimeConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class DateOnlyToDateTimeConverter : Converter<DateOnly, DateTime>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override DateTime Convert(DateOnly value, IFormatProvider? formatProvider)
        {
            return value.ToDateTime(TimeOnly.MinValue);
        }
    }
}
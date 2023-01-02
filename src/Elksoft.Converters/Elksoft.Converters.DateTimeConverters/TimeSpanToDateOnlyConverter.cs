// <copyright file="TimeSpanToDateOnlyConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class TimeSpanToDateOnlyConverter : Converter<TimeSpan, DateOnly>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override DateOnly Convert(TimeSpan value, IFormatProvider? formatProvider)
        {
            return DateOnly.FromDateTime(DateTime.MinValue.Add(value));
        }
    }
}
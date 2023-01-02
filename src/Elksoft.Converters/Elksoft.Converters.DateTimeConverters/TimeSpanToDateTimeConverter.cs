// <copyright file="TimeSpanToDateTimeConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class TimeSpanToDateTimeConverter : Converter<TimeSpan, DateTime>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override DateTime Convert(TimeSpan value, IFormatProvider? formatProvider)
        {
            return DateTime.MinValue.Add(value);
        }
    }
}
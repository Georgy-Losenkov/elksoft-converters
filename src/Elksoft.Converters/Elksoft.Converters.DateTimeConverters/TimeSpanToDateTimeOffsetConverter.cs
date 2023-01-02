// <copyright file="TimeSpanToDateTimeOffsetConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class TimeSpanToDateTimeOffsetConverter : Converter<TimeSpan, DateTimeOffset>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override DateTimeOffset Convert(TimeSpan value, IFormatProvider? formatProvider)
        {
            return DateTimeOffset.MinValue.Add(value);
        }
    }
}
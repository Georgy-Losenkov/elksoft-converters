// <copyright file="DateTimeOffsetToTimeSpanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class DateTimeOffsetToTimeSpanConverter : Converter<DateTimeOffset, TimeSpan>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override TimeSpan Convert(DateTimeOffset value, IFormatProvider? formatProvider)
        {
            return value.DateTime.TimeOfDay;
        }
    }
}
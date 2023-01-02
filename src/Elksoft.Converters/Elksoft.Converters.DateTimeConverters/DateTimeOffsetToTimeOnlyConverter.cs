// <copyright file="DateTimeOffsetToTimeOnlyConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class DateTimeOffsetToTimeOnlyConverter : Converter<DateTimeOffset, TimeOnly>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override TimeOnly Convert(DateTimeOffset value, IFormatProvider? formatProvider)
        {
            return TimeOnly.FromTimeSpan(value.DateTime.TimeOfDay);
        }
    }
}
// <copyright file="TimeSpanToTimeOnlyConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class TimeSpanToTimeOnlyConverter : Converter<TimeSpan, TimeOnly>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override TimeOnly Convert(TimeSpan value, IFormatProvider? formatProvider)
        {
            return TimeOnly.FromTimeSpan(value);
        }
    }
}
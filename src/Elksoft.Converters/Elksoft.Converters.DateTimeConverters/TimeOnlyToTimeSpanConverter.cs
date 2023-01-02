// <copyright file="TimeOnlyToTimeSpanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class TimeOnlyToTimeSpanConverter : Converter<TimeOnly, TimeSpan>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override TimeSpan Convert(TimeOnly value, IFormatProvider? formatProvider)
        {
            return value.ToTimeSpan();
        }
    }
}
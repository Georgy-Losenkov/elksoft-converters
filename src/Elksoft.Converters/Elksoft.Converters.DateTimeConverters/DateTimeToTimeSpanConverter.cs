// <copyright file="DateTimeToTimeSpanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class DateTimeToTimeSpanConverter : Converter<DateTime, TimeSpan>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override TimeSpan Convert(DateTime value, IFormatProvider? formatProvider)
        {
            return value.TimeOfDay;
        }
    }
}
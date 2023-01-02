// <copyright file="DateTimeToTimeOnlyConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class DateTimeToTimeOnlyConverter : Converter<DateTime, TimeOnly>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override TimeOnly Convert(DateTime value, IFormatProvider? formatProvider)
        {
            return TimeOnly.FromDateTime(value);
        }
    }
}
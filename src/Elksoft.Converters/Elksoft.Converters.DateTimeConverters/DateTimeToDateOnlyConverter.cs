// <copyright file="DateTimeToDateOnlyConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class DateTimeToDateOnlyConverter : Converter<DateTime, DateOnly>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override DateOnly Convert(DateTime value, IFormatProvider? formatProvider)
        {
            return DateOnly.FromDateTime(value);
        }
    }
}
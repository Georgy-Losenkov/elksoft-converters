// <copyright file="DateTimeOffsetToDateOnlyConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class DateTimeOffsetToDateOnlyConverter : Converter<DateTimeOffset, DateOnly>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override DateOnly Convert(DateTimeOffset value, IFormatProvider? formatProvider)
        {
            return DateOnly.FromDateTime(value.DateTime);
        }
    }
}
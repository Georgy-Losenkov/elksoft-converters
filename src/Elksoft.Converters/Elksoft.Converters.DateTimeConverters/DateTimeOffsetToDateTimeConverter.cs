// <copyright file="DateTimeOffsetToDateTimeConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class DateTimeOffsetToDateTimeConverter : Converter<DateTimeOffset, DateTime>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override DateTime Convert(DateTimeOffset value, IFormatProvider? formatProvider)
        {
            return value.DateTime;
        }
    }
}
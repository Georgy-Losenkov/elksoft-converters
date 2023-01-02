// <copyright file="DateTimeToDateTimeOffsetConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.DateTimeConverters
{
    internal sealed class DateTimeToDateTimeOffsetConverter : Converter<DateTime, DateTimeOffset>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override DateTimeOffset Convert(DateTime value, IFormatProvider? formatProvider)
        {
            return value;
        }
    }
}
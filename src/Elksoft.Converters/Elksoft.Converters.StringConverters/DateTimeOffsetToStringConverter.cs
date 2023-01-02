// <copyright file="DateTimeOffsetToStringConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class DateTimeOffsetToStringConverter : Converter<DateTimeOffset, String>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override String? Convert(DateTimeOffset value, IFormatProvider? formatProvider)
        {
            return value.ToString(null, formatProvider);
        }
    }
}

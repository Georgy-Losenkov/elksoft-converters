// <copyright file="DateOnlyToStringConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class DateOnlyToStringConverter : Converter<DateOnly, String>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override String? Convert(DateOnly value, IFormatProvider? formatProvider)
        {
            return value.ToString(null, formatProvider);
        }
    }
}

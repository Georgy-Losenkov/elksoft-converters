// <copyright file="StringToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToBooleanConverter : Converter<String, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Boolean Convert(String? value, IFormatProvider? formatProvider)
        {
            return Boolean.Parse(
                Check.NotNull(value, nameof(value)));
        }
    }
}

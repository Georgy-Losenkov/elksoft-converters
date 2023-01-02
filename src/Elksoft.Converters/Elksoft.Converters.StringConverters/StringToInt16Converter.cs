// <copyright file="StringToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToInt16Converter : Converter<String, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int16 Convert(String? value, IFormatProvider? formatProvider)
        {
            return Int16.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

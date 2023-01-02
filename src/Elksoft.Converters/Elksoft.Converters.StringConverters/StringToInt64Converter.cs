// <copyright file="StringToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToInt64Converter : Converter<String, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int64 Convert(String? value, IFormatProvider? formatProvider)
        {
            return Int64.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

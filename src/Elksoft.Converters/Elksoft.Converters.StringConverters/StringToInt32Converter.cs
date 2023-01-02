// <copyright file="StringToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToInt32Converter : Converter<String, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int32 Convert(String? value, IFormatProvider? formatProvider)
        {
            return Int32.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

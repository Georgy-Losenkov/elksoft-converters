// <copyright file="StringToHalfConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToHalfConverter : Converter<String, Half>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Half Convert(String? value, IFormatProvider? formatProvider)
        {
            return Half.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}
#endif

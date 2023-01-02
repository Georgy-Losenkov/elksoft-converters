// <copyright file="StringToInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToInt128Converter : Converter<String, Int128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int128 Convert(String? value, IFormatProvider? formatProvider)
        {
            return Int128.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}
#endif

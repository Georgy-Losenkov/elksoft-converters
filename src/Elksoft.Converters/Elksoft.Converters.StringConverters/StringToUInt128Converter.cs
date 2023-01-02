// <copyright file="StringToUInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToUInt128Converter : Converter<String, UInt128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt128 Convert(String? value, IFormatProvider? formatProvider)
        {
            return UInt128.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}
#endif

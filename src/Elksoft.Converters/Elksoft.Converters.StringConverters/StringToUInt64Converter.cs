// <copyright file="StringToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToUInt64Converter : Converter<String, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt64 Convert(String? value, IFormatProvider? formatProvider)
        {
            return UInt64.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

// <copyright file="StringToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToUInt32Converter : Converter<String, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt32 Convert(String? value, IFormatProvider? formatProvider)
        {
            return UInt32.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

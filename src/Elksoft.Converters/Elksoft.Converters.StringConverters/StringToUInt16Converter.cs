// <copyright file="StringToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToUInt16Converter : Converter<String, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(String? value, IFormatProvider? formatProvider)
        {
            return UInt16.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

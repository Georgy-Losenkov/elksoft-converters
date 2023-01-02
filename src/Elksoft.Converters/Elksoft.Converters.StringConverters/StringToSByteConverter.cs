// <copyright file="StringToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToSByteConverter : Converter<String, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(String? value, IFormatProvider? formatProvider)
        {
            return SByte.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

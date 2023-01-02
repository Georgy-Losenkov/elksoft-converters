// <copyright file="StringToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToByteConverter : Converter<String, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(String? value, IFormatProvider? formatProvider)
        {
            return Byte.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}

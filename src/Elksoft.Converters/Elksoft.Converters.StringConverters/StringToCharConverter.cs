// <copyright file="StringToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToCharConverter : Converter<String, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(String? value, IFormatProvider? formatProvider)
        {
            return Char.Parse(
                Check.NotNull(value, nameof(value)));
        }
    }
}

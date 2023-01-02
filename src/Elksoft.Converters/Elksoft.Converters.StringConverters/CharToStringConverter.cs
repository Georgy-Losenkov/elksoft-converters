// <copyright file="CharToStringConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class CharToStringConverter : Converter<Char, String>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override String? Convert(Char value, IFormatProvider? formatProvider)
        {
            return value.ToString();
        }
    }
}

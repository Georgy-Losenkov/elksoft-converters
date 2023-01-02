// <copyright file="CharToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class CharToBooleanConverter : Converter<Char, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(Char value, IFormatProvider? formatProvider)
        {
            return value != '\u0000';
        }
    }
}

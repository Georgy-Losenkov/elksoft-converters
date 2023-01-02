// <copyright file="BooleanToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToCharConverter : Converter<Boolean, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Char Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? '\u0001' : '\u0000';
        }
    }
}

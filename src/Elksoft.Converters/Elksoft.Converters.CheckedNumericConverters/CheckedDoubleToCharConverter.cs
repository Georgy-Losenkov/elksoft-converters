// <copyright file="CheckedDoubleToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedDoubleToCharConverter : Converter<Double, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(Double value, IFormatProvider? formatProvider)
        {
            return checked((Char)value);
        }
    }
}

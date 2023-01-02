// <copyright file="CheckedDecimalToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedDecimalToCharConverter : Converter<Decimal, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return checked((Char)value);
        }
    }
}

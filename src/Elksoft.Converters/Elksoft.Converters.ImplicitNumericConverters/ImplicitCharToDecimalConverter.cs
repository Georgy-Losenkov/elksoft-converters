// <copyright file="ImplicitCharToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitCharToDecimalConverter : Converter<Char, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Decimal Convert(Char value, IFormatProvider? formatProvider)
        {
            return unchecked((Decimal)value);
        }
    }
}

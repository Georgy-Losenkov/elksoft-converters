// <copyright file="ImplicitCharToDoubleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitCharToDoubleConverter : Converter<Char, Double>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Double Convert(Char value, IFormatProvider? formatProvider)
        {
            return unchecked((Double)value);
        }
    }
}

// <copyright file="ImplicitSingleToDoubleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitSingleToDoubleConverter : Converter<Single, Double>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Double Convert(Single value, IFormatProvider? formatProvider)
        {
            return unchecked((Double)value);
        }
    }
}

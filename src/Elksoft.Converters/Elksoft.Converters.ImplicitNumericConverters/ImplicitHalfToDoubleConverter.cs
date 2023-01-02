// <copyright file="ImplicitHalfToDoubleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitHalfToDoubleConverter : Converter<Half, Double>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Double Convert(Half value, IFormatProvider? formatProvider)
        {
            return unchecked((Double)value);
        }
    }
}
#endif

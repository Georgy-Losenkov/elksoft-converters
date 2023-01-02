// <copyright file="ImplicitUInt128ToDoubleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt128ToDoubleConverter : Converter<UInt128, Double>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Double Convert(UInt128 value, IFormatProvider? formatProvider)
        {
            return unchecked((Double)value);
        }
    }
}
#endif

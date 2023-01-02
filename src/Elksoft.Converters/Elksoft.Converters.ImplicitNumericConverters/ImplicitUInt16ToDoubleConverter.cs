// <copyright file="ImplicitUInt16ToDoubleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt16ToDoubleConverter : Converter<UInt16, Double>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Double Convert(UInt16 value, IFormatProvider? formatProvider)
        {
            return unchecked((Double)value);
        }
    }
}

// <copyright file="ImplicitUInt64ToDoubleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt64ToDoubleConverter : Converter<UInt64, Double>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Double Convert(UInt64 value, IFormatProvider? formatProvider)
        {
            return unchecked((Double)value);
        }
    }
}

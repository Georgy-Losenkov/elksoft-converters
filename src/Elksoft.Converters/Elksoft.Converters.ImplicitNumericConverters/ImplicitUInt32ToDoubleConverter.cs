// <copyright file="ImplicitUInt32ToDoubleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt32ToDoubleConverter : Converter<UInt32, Double>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Double Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Double)value);
        }
    }
}

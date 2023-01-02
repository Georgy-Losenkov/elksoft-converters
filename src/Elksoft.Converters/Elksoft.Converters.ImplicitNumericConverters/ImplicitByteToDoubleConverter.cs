// <copyright file="ImplicitByteToDoubleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitByteToDoubleConverter : Converter<Byte, Double>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Double Convert(Byte value, IFormatProvider? formatProvider)
        {
            return unchecked((Double)value);
        }
    }
}

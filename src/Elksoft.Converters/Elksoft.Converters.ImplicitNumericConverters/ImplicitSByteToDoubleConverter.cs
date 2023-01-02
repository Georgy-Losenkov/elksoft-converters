// <copyright file="ImplicitSByteToDoubleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitSByteToDoubleConverter : Converter<SByte, Double>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Double Convert(SByte value, IFormatProvider? formatProvider)
        {
            return unchecked((Double)value);
        }
    }
}

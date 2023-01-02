// <copyright file="ImplicitInt32ToDoubleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitInt32ToDoubleConverter : Converter<Int32, Double>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Double Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Double)value);
        }
    }
}

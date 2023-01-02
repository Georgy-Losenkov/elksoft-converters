// <copyright file="ImplicitInt64ToDoubleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitInt64ToDoubleConverter : Converter<Int64, Double>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Double Convert(Int64 value, IFormatProvider? formatProvider)
        {
            return unchecked((Double)value);
        }
    }
}

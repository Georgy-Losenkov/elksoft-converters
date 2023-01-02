// <copyright file="BooleanToDoubleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToDoubleConverter : Converter<Boolean, Double>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Double Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? 1.0 : 0.0;
        }
    }
}

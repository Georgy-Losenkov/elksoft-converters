// <copyright file="DoubleToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class DoubleToBooleanConverter : Converter<Double, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(Double value, IFormatProvider? formatProvider)
        {
            return value != 0.0;
        }
    }
}

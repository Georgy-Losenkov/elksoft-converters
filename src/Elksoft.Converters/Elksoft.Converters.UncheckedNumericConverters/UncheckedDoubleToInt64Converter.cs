﻿// <copyright file="UncheckedDoubleToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedDoubleToInt64Converter : Converter<Double, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int64 Convert(Double value, IFormatProvider? formatProvider)
        {
            return unchecked((Int64)value);
        }
    }
}

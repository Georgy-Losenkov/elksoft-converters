// <copyright file="UncheckedSingleToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedSingleToInt64Converter : Converter<Single, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int64 Convert(Single value, IFormatProvider? formatProvider)
        {
            return unchecked((Int64)value);
        }
    }
}

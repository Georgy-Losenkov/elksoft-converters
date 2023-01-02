// <copyright file="UncheckedInt64ToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt64ToInt32Converter : Converter<Int64, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int32 Convert(Int64 value, IFormatProvider? formatProvider)
        {
            return unchecked((Int32)value);
        }
    }
}

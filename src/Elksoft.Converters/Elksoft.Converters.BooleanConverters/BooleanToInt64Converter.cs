// <copyright file="BooleanToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToInt64Converter : Converter<Boolean, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int64 Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? 1L : 0L;
        }
    }
}

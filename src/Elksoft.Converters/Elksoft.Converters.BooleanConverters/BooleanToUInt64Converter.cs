// <copyright file="BooleanToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToUInt64Converter : Converter<Boolean, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt64 Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? 1UL : 0UL;
        }
    }
}

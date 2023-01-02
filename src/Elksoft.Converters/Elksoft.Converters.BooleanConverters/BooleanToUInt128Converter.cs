// <copyright file="BooleanToUInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToUInt128Converter : Converter<Boolean, UInt128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt128 Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? UInt128.One : UInt128.Zero;
        }
    }
}
#endif

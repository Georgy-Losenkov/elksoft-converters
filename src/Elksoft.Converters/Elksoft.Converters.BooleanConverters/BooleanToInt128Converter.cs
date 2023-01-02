// <copyright file="BooleanToInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToInt128Converter : Converter<Boolean, Int128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int128 Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? Int128.One : Int128.Zero;
        }
    }
}
#endif

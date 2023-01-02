// <copyright file="UInt128ToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class UInt128ToBooleanConverter : Converter<UInt128, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(UInt128 value, IFormatProvider? formatProvider)
        {
            return value != UInt128.Zero;
        }
    }
}
#endif

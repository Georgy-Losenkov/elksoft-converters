// <copyright file="Int128ToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class Int128ToBooleanConverter : Converter<Int128, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(Int128 value, IFormatProvider? formatProvider)
        {
            return value != Int128.Zero;
        }
    }
}
#endif

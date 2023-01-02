// <copyright file="HalfToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class HalfToBooleanConverter : Converter<Half, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(Half value, IFormatProvider? formatProvider)
        {
            return value != Half.Zero;
        }
    }
}
#endif

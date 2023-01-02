// <copyright file="BooleanToHalfConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToHalfConverter : Converter<Boolean, Half>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Half Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? Half.One : Half.Zero;
        }
    }
}
#endif

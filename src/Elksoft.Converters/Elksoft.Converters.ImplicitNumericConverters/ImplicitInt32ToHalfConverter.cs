// <copyright file="ImplicitInt32ToHalfConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitInt32ToHalfConverter : Converter<Int32, Half>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Half Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Half)value);
        }
    }
}
#endif

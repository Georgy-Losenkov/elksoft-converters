// <copyright file="ImplicitHalfToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitHalfToInt32Converter : Converter<Half, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int32 Convert(Half value, IFormatProvider? formatProvider)
        {
            return unchecked((Int32)value);
        }
    }
}
#endif

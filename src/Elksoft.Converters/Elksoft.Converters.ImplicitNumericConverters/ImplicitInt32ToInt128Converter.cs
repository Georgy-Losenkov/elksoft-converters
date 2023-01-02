// <copyright file="ImplicitInt32ToInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitInt32ToInt128Converter : Converter<Int32, Int128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int128 Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Int128)value);
        }
    }
}
#endif

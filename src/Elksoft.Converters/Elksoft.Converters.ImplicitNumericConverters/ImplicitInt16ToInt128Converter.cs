// <copyright file="ImplicitInt16ToInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitInt16ToInt128Converter : Converter<Int16, Int128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int128 Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return unchecked((Int128)value);
        }
    }
}
#endif

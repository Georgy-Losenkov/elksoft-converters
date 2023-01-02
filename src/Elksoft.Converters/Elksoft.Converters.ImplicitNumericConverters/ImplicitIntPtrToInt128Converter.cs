// <copyright file="ImplicitIntPtrToInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitIntPtrToInt128Converter : Converter<IntPtr, Int128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int128 Convert(IntPtr value, IFormatProvider? formatProvider)
        {
            return unchecked((Int128)value);
        }
    }
}
#endif

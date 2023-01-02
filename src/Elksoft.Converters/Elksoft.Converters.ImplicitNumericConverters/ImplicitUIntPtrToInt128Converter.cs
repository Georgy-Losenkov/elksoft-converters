// <copyright file="ImplicitUIntPtrToInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUIntPtrToInt128Converter : Converter<UIntPtr, Int128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int128 Convert(UIntPtr value, IFormatProvider? formatProvider)
        {
            return unchecked((Int128)value);
        }
    }
}
#endif

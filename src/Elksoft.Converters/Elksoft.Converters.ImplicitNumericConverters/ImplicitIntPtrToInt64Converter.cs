// <copyright file="ImplicitIntPtrToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitIntPtrToInt64Converter : Converter<IntPtr, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int64 Convert(IntPtr value, IFormatProvider? formatProvider)
        {
            return unchecked((Int64)value);
        }
    }
}
#endif

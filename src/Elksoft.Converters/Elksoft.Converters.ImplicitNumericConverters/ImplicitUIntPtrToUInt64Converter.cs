// <copyright file="ImplicitUIntPtrToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUIntPtrToUInt64Converter : Converter<UIntPtr, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt64 Convert(UIntPtr value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt64)value);
        }
    }
}
#endif

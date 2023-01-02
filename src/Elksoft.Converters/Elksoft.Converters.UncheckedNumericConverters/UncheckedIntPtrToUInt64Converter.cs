// <copyright file="UncheckedIntPtrToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedIntPtrToUInt64Converter : Converter<IntPtr, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt64 Convert(IntPtr value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt64)value);
        }
    }
}
#endif

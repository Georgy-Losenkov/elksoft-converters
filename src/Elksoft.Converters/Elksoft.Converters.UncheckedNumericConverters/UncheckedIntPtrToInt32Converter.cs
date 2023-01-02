// <copyright file="UncheckedIntPtrToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedIntPtrToInt32Converter : Converter<IntPtr, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int32 Convert(IntPtr value, IFormatProvider? formatProvider)
        {
            return unchecked((Int32)value);
        }
    }
}
#endif

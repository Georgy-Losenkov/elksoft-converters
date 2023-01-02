// <copyright file="UncheckedInt128ToIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt128ToIntPtrConverter : Converter<Int128, IntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override IntPtr Convert(Int128 value, IFormatProvider? formatProvider)
        {
            return unchecked((IntPtr)value);
        }
    }
}
#endif

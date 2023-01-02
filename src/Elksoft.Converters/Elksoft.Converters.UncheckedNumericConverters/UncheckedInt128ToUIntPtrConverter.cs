// <copyright file="UncheckedInt128ToUIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt128ToUIntPtrConverter : Converter<Int128, UIntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UIntPtr Convert(Int128 value, IFormatProvider? formatProvider)
        {
            return unchecked((UIntPtr)value);
        }
    }
}
#endif

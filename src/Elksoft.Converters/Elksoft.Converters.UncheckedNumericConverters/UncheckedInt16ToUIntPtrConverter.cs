// <copyright file="UncheckedInt16ToUIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt16ToUIntPtrConverter : Converter<Int16, UIntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UIntPtr Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return unchecked((UIntPtr)value);
        }
    }
}
#endif

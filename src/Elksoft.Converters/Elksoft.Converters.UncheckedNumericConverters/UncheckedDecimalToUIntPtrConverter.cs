// <copyright file="UncheckedDecimalToUIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedDecimalToUIntPtrConverter : Converter<Decimal, UIntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UIntPtr Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return unchecked((UIntPtr)value);
        }
    }
}
#endif

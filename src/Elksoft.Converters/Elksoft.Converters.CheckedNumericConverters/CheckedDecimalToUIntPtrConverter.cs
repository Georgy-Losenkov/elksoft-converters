// <copyright file="CheckedDecimalToUIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedDecimalToUIntPtrConverter : Converter<Decimal, UIntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UIntPtr Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return checked((UIntPtr)value);
        }
    }
}
#endif

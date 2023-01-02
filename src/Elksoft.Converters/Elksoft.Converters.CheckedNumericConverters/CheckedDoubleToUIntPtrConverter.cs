// <copyright file="CheckedDoubleToUIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedDoubleToUIntPtrConverter : Converter<Double, UIntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UIntPtr Convert(Double value, IFormatProvider? formatProvider)
        {
            return checked((UIntPtr)value);
        }
    }
}
#endif

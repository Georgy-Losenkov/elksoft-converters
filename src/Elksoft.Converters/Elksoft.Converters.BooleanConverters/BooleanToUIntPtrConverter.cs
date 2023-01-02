// <copyright file="BooleanToUIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToUIntPtrConverter : Converter<Boolean, UIntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UIntPtr Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? new UIntPtr(1) : UIntPtr.Zero;
        }
    }
}
#endif

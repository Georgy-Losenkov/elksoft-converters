// <copyright file="BooleanToIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToIntPtrConverter : Converter<Boolean, IntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override IntPtr Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? new IntPtr(1) : IntPtr.Zero;
        }
    }
}
#endif

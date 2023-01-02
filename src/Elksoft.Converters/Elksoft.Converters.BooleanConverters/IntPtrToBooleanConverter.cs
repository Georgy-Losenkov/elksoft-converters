// <copyright file="IntPtrToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class IntPtrToBooleanConverter : Converter<IntPtr, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(IntPtr value, IFormatProvider? formatProvider)
        {
            return value != IntPtr.Zero;
        }
    }
}
#endif

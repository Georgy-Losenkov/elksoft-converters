// <copyright file="CheckedUInt64ToIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt64ToIntPtrConverter : Converter<UInt64, IntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override IntPtr Convert(UInt64 value, IFormatProvider? formatProvider)
        {
            return checked((IntPtr)value);
        }
    }
}
#endif

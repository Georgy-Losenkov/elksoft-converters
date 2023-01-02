// <copyright file="CheckedUInt32ToIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt32ToIntPtrConverter : Converter<UInt32, IntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override IntPtr Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return checked((IntPtr)value);
        }
    }
}
#endif

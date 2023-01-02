// <copyright file="CheckedInt64ToIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt64ToIntPtrConverter : Converter<Int64, IntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override IntPtr Convert(Int64 value, IFormatProvider? formatProvider)
        {
            return checked((IntPtr)value);
        }
    }
}
#endif

// <copyright file="CheckedIntPtrToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedIntPtrToUInt16Converter : Converter<IntPtr, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(IntPtr value, IFormatProvider? formatProvider)
        {
            return checked((UInt16)value);
        }
    }
}
#endif

// <copyright file="CheckedIntPtrToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedIntPtrToInt32Converter : Converter<IntPtr, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int32 Convert(IntPtr value, IFormatProvider? formatProvider)
        {
            return checked((Int32)value);
        }
    }
}
#endif

// <copyright file="CheckedUIntPtrToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUIntPtrToInt32Converter : Converter<UIntPtr, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int32 Convert(UIntPtr value, IFormatProvider? formatProvider)
        {
            return checked((Int32)value);
        }
    }
}
#endif

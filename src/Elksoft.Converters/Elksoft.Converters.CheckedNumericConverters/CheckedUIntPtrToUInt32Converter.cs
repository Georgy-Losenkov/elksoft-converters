// <copyright file="CheckedUIntPtrToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUIntPtrToUInt32Converter : Converter<UIntPtr, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt32 Convert(UIntPtr value, IFormatProvider? formatProvider)
        {
            return checked((UInt32)value);
        }
    }
}
#endif

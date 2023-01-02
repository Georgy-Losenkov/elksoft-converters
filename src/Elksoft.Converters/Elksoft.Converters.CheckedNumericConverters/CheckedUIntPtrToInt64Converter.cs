// <copyright file="CheckedUIntPtrToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUIntPtrToInt64Converter : Converter<UIntPtr, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int64 Convert(UIntPtr value, IFormatProvider? formatProvider)
        {
            return checked((Int64)value);
        }
    }
}
#endif

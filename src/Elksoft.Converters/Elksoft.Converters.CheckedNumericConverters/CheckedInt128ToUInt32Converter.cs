// <copyright file="CheckedInt128ToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt128ToUInt32Converter : Converter<Int128, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt32 Convert(Int128 value, IFormatProvider? formatProvider)
        {
            return checked((UInt32)value);
        }
    }
}
#endif

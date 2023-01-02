// <copyright file="ImplicitUInt64ToUInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt64ToUInt128Converter : Converter<UInt64, UInt128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt128 Convert(UInt64 value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt128)value);
        }
    }
}
#endif

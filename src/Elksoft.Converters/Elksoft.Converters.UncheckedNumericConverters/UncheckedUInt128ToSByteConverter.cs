// <copyright file="UncheckedUInt128ToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedUInt128ToSByteConverter : Converter<UInt128, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(UInt128 value, IFormatProvider? formatProvider)
        {
            return unchecked((SByte)value);
        }
    }
}
#endif

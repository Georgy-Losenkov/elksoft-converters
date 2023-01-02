// <copyright file="UncheckedUInt16ToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedUInt16ToSByteConverter : Converter<UInt16, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(UInt16 value, IFormatProvider? formatProvider)
        {
            return unchecked((SByte)value);
        }
    }
}

// <copyright file="UncheckedUInt32ToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedUInt32ToSByteConverter : Converter<UInt32, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return unchecked((SByte)value);
        }
    }
}

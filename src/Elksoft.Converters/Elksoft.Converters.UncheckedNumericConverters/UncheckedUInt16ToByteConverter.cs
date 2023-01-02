// <copyright file="UncheckedUInt16ToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedUInt16ToByteConverter : Converter<UInt16, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(UInt16 value, IFormatProvider? formatProvider)
        {
            return unchecked((Byte)value);
        }
    }
}

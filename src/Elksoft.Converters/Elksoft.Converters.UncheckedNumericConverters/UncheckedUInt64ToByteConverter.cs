// <copyright file="UncheckedUInt64ToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedUInt64ToByteConverter : Converter<UInt64, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(UInt64 value, IFormatProvider? formatProvider)
        {
            return unchecked((Byte)value);
        }
    }
}

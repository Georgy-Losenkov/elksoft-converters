// <copyright file="UncheckedUInt32ToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedUInt32ToByteConverter : Converter<UInt32, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Byte)value);
        }
    }
}

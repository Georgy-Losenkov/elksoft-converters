// <copyright file="CheckedUInt32ToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt32ToByteConverter : Converter<UInt32, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return checked((Byte)value);
        }
    }
}

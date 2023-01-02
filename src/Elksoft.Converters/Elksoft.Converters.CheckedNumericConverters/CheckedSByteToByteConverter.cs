// <copyright file="CheckedSByteToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedSByteToByteConverter : Converter<SByte, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(SByte value, IFormatProvider? formatProvider)
        {
            return checked((Byte)value);
        }
    }
}

// <copyright file="CheckedByteToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedByteToSByteConverter : Converter<Byte, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Byte value, IFormatProvider? formatProvider)
        {
            return checked((SByte)value);
        }
    }
}

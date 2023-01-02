// <copyright file="ImplicitByteToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitByteToUInt16Converter : Converter<Byte, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt16 Convert(Byte value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt16)value);
        }
    }
}

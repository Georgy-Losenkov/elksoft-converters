// <copyright file="ImplicitByteToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitByteToUInt32Converter : Converter<Byte, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt32 Convert(Byte value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt32)value);
        }
    }
}

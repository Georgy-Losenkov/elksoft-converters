// <copyright file="ImplicitByteToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitByteToUInt64Converter : Converter<Byte, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt64 Convert(Byte value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt64)value);
        }
    }
}

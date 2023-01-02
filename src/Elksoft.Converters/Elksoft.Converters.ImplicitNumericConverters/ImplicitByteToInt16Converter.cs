// <copyright file="ImplicitByteToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitByteToInt16Converter : Converter<Byte, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int16 Convert(Byte value, IFormatProvider? formatProvider)
        {
            return unchecked((Int16)value);
        }
    }
}

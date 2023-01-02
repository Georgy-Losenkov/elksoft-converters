// <copyright file="ImplicitByteToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitByteToInt32Converter : Converter<Byte, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int32 Convert(Byte value, IFormatProvider? formatProvider)
        {
            return unchecked((Int32)value);
        }
    }
}

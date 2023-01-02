// <copyright file="ImplicitByteToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitByteToCharConverter : Converter<Byte, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Char Convert(Byte value, IFormatProvider? formatProvider)
        {
            return unchecked((Char)value);
        }
    }
}

// <copyright file="ImplicitCharToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitCharToUInt16Converter : Converter<Char, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt16 Convert(Char value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt16)value);
        }
    }
}

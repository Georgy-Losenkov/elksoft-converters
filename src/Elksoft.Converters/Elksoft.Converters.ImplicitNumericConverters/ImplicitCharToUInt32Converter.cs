// <copyright file="ImplicitCharToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitCharToUInt32Converter : Converter<Char, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt32 Convert(Char value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt32)value);
        }
    }
}

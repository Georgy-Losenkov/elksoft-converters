// <copyright file="ImplicitCharToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitCharToUInt64Converter : Converter<Char, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt64 Convert(Char value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt64)value);
        }
    }
}

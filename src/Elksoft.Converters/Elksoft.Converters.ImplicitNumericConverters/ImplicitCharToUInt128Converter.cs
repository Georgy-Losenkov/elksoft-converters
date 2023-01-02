// <copyright file="ImplicitCharToUInt128Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitCharToUInt128Converter : Converter<Char, UInt128>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt128 Convert(Char value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt128)value);
        }
    }
}
#endif

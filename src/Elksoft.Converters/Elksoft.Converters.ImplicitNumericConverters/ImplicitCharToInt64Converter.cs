// <copyright file="ImplicitCharToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitCharToInt64Converter : Converter<Char, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int64 Convert(Char value, IFormatProvider? formatProvider)
        {
            return unchecked((Int64)value);
        }
    }
}

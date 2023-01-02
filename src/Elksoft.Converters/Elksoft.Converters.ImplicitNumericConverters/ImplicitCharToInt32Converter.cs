// <copyright file="ImplicitCharToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitCharToInt32Converter : Converter<Char, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int32 Convert(Char value, IFormatProvider? formatProvider)
        {
            return unchecked((Int32)value);
        }
    }
}

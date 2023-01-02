// <copyright file="ImplicitUInt16ToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt16ToCharConverter : Converter<UInt16, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Char Convert(UInt16 value, IFormatProvider? formatProvider)
        {
            return unchecked((Char)value);
        }
    }
}

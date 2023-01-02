// <copyright file="CheckedInt16ToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt16ToCharConverter : Converter<Int16, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return checked((Char)value);
        }
    }
}

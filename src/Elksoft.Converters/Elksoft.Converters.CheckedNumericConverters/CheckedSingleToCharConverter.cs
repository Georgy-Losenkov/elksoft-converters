// <copyright file="CheckedSingleToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedSingleToCharConverter : Converter<Single, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(Single value, IFormatProvider? formatProvider)
        {
            return checked((Char)value);
        }
    }
}

// <copyright file="CheckedSByteToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedSByteToCharConverter : Converter<SByte, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(SByte value, IFormatProvider? formatProvider)
        {
            return checked((Char)value);
        }
    }
}

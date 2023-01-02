// <copyright file="CheckedUInt64ToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt64ToCharConverter : Converter<UInt64, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(UInt64 value, IFormatProvider? formatProvider)
        {
            return checked((Char)value);
        }
    }
}

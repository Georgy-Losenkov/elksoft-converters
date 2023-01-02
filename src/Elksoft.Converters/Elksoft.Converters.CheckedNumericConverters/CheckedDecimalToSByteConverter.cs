// <copyright file="CheckedDecimalToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedDecimalToSByteConverter : Converter<Decimal, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return checked((SByte)value);
        }
    }
}

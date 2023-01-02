// <copyright file="UncheckedDecimalToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedDecimalToSByteConverter : Converter<Decimal, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return unchecked((SByte)value);
        }
    }
}

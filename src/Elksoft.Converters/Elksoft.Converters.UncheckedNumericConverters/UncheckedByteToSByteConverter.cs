// <copyright file="UncheckedByteToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedByteToSByteConverter : Converter<Byte, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Byte value, IFormatProvider? formatProvider)
        {
            return unchecked((SByte)value);
        }
    }
}

// <copyright file="UncheckedCharToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedCharToSByteConverter : Converter<Char, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Char value, IFormatProvider? formatProvider)
        {
            return unchecked((SByte)value);
        }
    }
}

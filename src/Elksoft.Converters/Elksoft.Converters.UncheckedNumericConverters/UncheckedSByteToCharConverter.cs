// <copyright file="UncheckedSByteToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedSByteToCharConverter : Converter<SByte, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(SByte value, IFormatProvider? formatProvider)
        {
            return unchecked((Char)value);
        }
    }
}

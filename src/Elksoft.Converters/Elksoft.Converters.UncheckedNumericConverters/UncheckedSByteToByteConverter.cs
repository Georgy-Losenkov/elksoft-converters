// <copyright file="UncheckedSByteToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedSByteToByteConverter : Converter<SByte, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(SByte value, IFormatProvider? formatProvider)
        {
            return unchecked((Byte)value);
        }
    }
}

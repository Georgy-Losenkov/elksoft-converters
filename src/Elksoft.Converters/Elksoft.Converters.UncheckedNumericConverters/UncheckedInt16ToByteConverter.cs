// <copyright file="UncheckedInt16ToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt16ToByteConverter : Converter<Int16, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return unchecked((Byte)value);
        }
    }
}

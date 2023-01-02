// <copyright file="UncheckedInt64ToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt64ToByteConverter : Converter<Int64, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(Int64 value, IFormatProvider? formatProvider)
        {
            return unchecked((Byte)value);
        }
    }
}

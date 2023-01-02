// <copyright file="UncheckedInt32ToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt32ToByteConverter : Converter<Int32, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Byte)value);
        }
    }
}

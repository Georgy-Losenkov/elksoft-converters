// <copyright file="BooleanToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToByteConverter : Converter<Boolean, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Byte Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? (Byte)1 : (Byte)0;
        }
    }
}

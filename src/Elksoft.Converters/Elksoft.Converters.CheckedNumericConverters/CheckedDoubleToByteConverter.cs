// <copyright file="CheckedDoubleToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedDoubleToByteConverter : Converter<Double, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(Double value, IFormatProvider? formatProvider)
        {
            return checked((Byte)value);
        }
    }
}

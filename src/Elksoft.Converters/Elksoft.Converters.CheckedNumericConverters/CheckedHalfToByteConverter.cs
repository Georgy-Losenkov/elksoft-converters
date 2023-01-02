// <copyright file="CheckedHalfToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedHalfToByteConverter : Converter<Half, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(Half value, IFormatProvider? formatProvider)
        {
            return checked((Byte)value);
        }
    }
}
#endif

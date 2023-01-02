// <copyright file="CheckedInt16ToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt16ToSByteConverter : Converter<Int16, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return checked((SByte)value);
        }
    }
}

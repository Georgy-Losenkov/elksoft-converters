// <copyright file="CheckedInt64ToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt64ToSByteConverter : Converter<Int64, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Int64 value, IFormatProvider? formatProvider)
        {
            return checked((SByte)value);
        }
    }
}

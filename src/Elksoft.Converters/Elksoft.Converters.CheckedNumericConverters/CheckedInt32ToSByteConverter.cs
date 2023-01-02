// <copyright file="CheckedInt32ToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt32ToSByteConverter : Converter<Int32, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return checked((SByte)value);
        }
    }
}

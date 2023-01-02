// <copyright file="CheckedInt128ToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt128ToSByteConverter : Converter<Int128, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Int128 value, IFormatProvider? formatProvider)
        {
            return checked((SByte)value);
        }
    }
}
#endif

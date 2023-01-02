// <copyright file="UncheckedInt16ToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt16ToSByteConverter : Converter<Int16, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return unchecked((SByte)value);
        }
    }
}

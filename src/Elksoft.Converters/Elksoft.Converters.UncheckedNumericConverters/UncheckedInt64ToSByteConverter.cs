// <copyright file="UncheckedInt64ToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt64ToSByteConverter : Converter<Int64, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Int64 value, IFormatProvider? formatProvider)
        {
            return unchecked((SByte)value);
        }
    }
}

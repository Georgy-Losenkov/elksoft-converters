// <copyright file="BooleanToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToSByteConverter : Converter<Boolean, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override SByte Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? (SByte)1 : (SByte)0;
        }
    }
}

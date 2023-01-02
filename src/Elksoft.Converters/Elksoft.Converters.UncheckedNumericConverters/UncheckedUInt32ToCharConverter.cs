// <copyright file="UncheckedUInt32ToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedUInt32ToCharConverter : Converter<UInt32, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Char)value);
        }
    }
}

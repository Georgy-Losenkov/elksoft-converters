// <copyright file="UncheckedInt64ToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt64ToCharConverter : Converter<Int64, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(Int64 value, IFormatProvider? formatProvider)
        {
            return unchecked((Char)value);
        }
    }
}

// <copyright file="UncheckedInt16ToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt16ToCharConverter : Converter<Int16, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return unchecked((Char)value);
        }
    }
}

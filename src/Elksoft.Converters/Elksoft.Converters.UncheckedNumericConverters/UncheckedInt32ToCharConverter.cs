// <copyright file="UncheckedInt32ToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt32ToCharConverter : Converter<Int32, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Char)value);
        }
    }
}

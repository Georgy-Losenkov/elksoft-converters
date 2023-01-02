// <copyright file="UncheckedSingleToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedSingleToCharConverter : Converter<Single, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(Single value, IFormatProvider? formatProvider)
        {
            return unchecked((Char)value);
        }
    }
}

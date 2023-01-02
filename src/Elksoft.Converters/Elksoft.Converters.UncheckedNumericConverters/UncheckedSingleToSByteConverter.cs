// <copyright file="UncheckedSingleToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedSingleToSByteConverter : Converter<Single, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(Single value, IFormatProvider? formatProvider)
        {
            return unchecked((SByte)value);
        }
    }
}

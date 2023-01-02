// <copyright file="ImplicitSByteToHalfConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitSByteToHalfConverter : Converter<SByte, Half>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Half Convert(SByte value, IFormatProvider? formatProvider)
        {
            return unchecked((Half)value);
        }
    }
}
#endif

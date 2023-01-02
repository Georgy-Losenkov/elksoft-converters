// <copyright file="ImplicitHalfToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitHalfToDecimalConverter : Converter<Half, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Decimal Convert(Half value, IFormatProvider? formatProvider)
        {
            return unchecked((Decimal)value);
        }
    }
}
#endif

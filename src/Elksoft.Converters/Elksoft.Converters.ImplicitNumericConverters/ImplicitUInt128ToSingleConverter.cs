// <copyright file="ImplicitUInt128ToSingleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt128ToSingleConverter : Converter<UInt128, Single>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Single Convert(UInt128 value, IFormatProvider? formatProvider)
        {
            return unchecked((Single)value);
        }
    }
}
#endif

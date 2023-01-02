// <copyright file="ImplicitInt128ToSingleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitInt128ToSingleConverter : Converter<Int128, Single>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Single Convert(Int128 value, IFormatProvider? formatProvider)
        {
            return unchecked((Single)value);
        }
    }
}
#endif

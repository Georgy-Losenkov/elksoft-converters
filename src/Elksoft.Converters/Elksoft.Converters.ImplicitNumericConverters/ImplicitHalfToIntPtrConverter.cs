// <copyright file="ImplicitHalfToIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitHalfToIntPtrConverter : Converter<Half, IntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override IntPtr Convert(Half value, IFormatProvider? formatProvider)
        {
            return unchecked((IntPtr)value);
        }
    }
}
#endif

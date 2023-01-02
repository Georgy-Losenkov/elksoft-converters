// <copyright file="ImplicitIntPtrToSingleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitIntPtrToSingleConverter : Converter<IntPtr, Single>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Single Convert(IntPtr value, IFormatProvider? formatProvider)
        {
            return unchecked((Single)value);
        }
    }
}
#endif

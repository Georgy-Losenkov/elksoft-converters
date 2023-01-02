// <copyright file="ImplicitUIntPtrToSingleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUIntPtrToSingleConverter : Converter<UIntPtr, Single>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Single Convert(UIntPtr value, IFormatProvider? formatProvider)
        {
            return unchecked((Single)value);
        }
    }
}
#endif

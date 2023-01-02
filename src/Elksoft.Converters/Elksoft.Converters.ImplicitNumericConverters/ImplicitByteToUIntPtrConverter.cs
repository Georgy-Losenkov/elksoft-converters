// <copyright file="ImplicitByteToUIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitByteToUIntPtrConverter : Converter<Byte, UIntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UIntPtr Convert(Byte value, IFormatProvider? formatProvider)
        {
            return unchecked((UIntPtr)value);
        }
    }
}
#endif

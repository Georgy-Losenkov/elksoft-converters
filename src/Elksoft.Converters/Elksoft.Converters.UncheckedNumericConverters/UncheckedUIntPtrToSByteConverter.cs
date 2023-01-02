// <copyright file="UncheckedUIntPtrToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedUIntPtrToSByteConverter : Converter<UIntPtr, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(UIntPtr value, IFormatProvider? formatProvider)
        {
            return unchecked((SByte)value);
        }
    }
}
#endif

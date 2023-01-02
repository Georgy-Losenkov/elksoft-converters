// <copyright file="UncheckedUIntPtrToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedUIntPtrToByteConverter : Converter<UIntPtr, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(UIntPtr value, IFormatProvider? formatProvider)
        {
            return unchecked((Byte)value);
        }
    }
}
#endif

// <copyright file="CheckedUIntPtrToByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUIntPtrToByteConverter : Converter<UIntPtr, Byte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte Convert(UIntPtr value, IFormatProvider? formatProvider)
        {
            return checked((Byte)value);
        }
    }
}
#endif

// <copyright file="CheckedIntPtrToSByteConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedIntPtrToSByteConverter : Converter<IntPtr, SByte>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override SByte Convert(IntPtr value, IFormatProvider? formatProvider)
        {
            return checked((SByte)value);
        }
    }
}
#endif

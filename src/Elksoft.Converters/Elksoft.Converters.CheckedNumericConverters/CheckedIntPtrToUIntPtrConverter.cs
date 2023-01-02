// <copyright file="CheckedIntPtrToUIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedIntPtrToUIntPtrConverter : Converter<IntPtr, UIntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UIntPtr Convert(IntPtr value, IFormatProvider? formatProvider)
        {
            return checked((UIntPtr)value);
        }
    }
}
#endif

// <copyright file="UIntPtrToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class UIntPtrToBooleanConverter : Converter<UIntPtr, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(UIntPtr value, IFormatProvider? formatProvider)
        {
            return value != UIntPtr.Zero;
        }
    }
}
#endif

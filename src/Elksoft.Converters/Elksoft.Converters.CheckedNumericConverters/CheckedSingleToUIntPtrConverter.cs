// <copyright file="CheckedSingleToUIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedSingleToUIntPtrConverter : Converter<Single, UIntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UIntPtr Convert(Single value, IFormatProvider? formatProvider)
        {
            return checked((UIntPtr)value);
        }
    }
}
#endif

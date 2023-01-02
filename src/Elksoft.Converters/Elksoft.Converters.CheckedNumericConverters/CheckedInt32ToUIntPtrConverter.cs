// <copyright file="CheckedInt32ToUIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt32ToUIntPtrConverter : Converter<Int32, UIntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UIntPtr Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return checked((UIntPtr)value);
        }
    }
}
#endif

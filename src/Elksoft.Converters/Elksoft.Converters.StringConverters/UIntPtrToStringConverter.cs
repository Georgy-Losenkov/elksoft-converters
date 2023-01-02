// <copyright file="UIntPtrToStringConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class UIntPtrToStringConverter : Converter<UIntPtr, String>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override String? Convert(UIntPtr value, IFormatProvider? formatProvider)
        {
            return value.ToString(null, formatProvider);
        }
    }
}
#endif

// <copyright file="StringToUIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToUIntPtrConverter : Converter<String, UIntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UIntPtr Convert(String? value, IFormatProvider? formatProvider)
        {
            return UIntPtr.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}
#endif

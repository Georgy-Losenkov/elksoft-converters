// <copyright file="StringToIntPtrConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToIntPtrConverter : Converter<String, IntPtr>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override IntPtr Convert(String? value, IFormatProvider? formatProvider)
        {
            return IntPtr.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}
#endif

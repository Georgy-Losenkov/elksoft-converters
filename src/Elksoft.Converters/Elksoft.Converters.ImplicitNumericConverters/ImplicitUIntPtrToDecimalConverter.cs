// <copyright file="ImplicitUIntPtrToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUIntPtrToDecimalConverter : Converter<UIntPtr, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Decimal Convert(UIntPtr value, IFormatProvider? formatProvider)
        {
            return unchecked((Decimal)value);
        }
    }
}
#endif

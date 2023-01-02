// <copyright file="ImplicitInt16ToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitInt16ToInt32Converter : Converter<Int16, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int32 Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return unchecked((Int32)value);
        }
    }
}

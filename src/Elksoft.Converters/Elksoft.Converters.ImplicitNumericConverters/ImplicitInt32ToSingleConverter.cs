// <copyright file="ImplicitInt32ToSingleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitInt32ToSingleConverter : Converter<Int32, Single>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Single Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Single)value);
        }
    }
}

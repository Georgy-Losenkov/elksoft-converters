// <copyright file="CheckedInt32ToCharConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt32ToCharConverter : Converter<Int32, Char>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Char Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return checked((Char)value);
        }
    }
}

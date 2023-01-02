// <copyright file="GuidToBinaryConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BinaryConverters
{
    internal sealed class GuidToBinaryConverter : Converter<Guid, Byte[]>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Byte[] Convert(Guid value, IFormatProvider? formatProvider)
        {
            return value.ToByteArray();
        }
    }
}
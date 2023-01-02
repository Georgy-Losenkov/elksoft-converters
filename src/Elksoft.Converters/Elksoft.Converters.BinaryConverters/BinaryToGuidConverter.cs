// <copyright file="BinaryToGuidConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Threading;

namespace Elksoft.Converters.BinaryConverters
{
    internal sealed class BinaryToGuidConverter : Converter<Byte[], Guid>
    {
        private static readonly ThreadLocal<Byte[]> s_bufferCache = new ThreadLocal<Byte[]>(() => new Byte[16]);

        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Guid Convert(Byte[]? value, IFormatProvider? formatProvider)
        {
            if (value == null)
            {
                return default;
            }

            if (value.Length == 16)
            {
                return new Guid(value);
            }
            else
            {
                var bytes = s_bufferCache.Value;
                var count = Math.Min(value.Length, bytes!.Length);
                Array.Copy(value, 0, bytes, 0, count);
                Array.Clear(bytes, count, bytes.Length - count);
                var result = new Guid(bytes);
                return result;
            }
        }
    }
}
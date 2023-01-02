#nullable enable
using System;
using Elksoft.Converters;

namespace Elksoft.Samples
{
    public class SampleClass
    {
        public SampleClass(Int16 value)
        {
            Value = value;
        }

        public Int16 Value { get; }

        public static implicit operator SampleClass?(Int16? value)
        {
            if (value == null)
            {
                return null;
            }

            return new SampleClass(value.GetValueOrDefault());
        }

        public static implicit operator Int16?(SampleClass? value)
        {
            if (value == null)
            {
                return null;
            }

            return value.Value;
        }

        [RejectsNull]
        public static implicit operator Int32(SampleClass value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Value;
        }

        public static explicit operator SampleClass(Int32 value)
        {
            return new SampleClass(checked((Int16)value));
        }

        [RejectsNull]
        public static explicit operator Byte(SampleClass value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return checked((Byte)value.Value);
        }

        public static implicit operator SampleClass(Byte value)
        {
            return new SampleClass(value);
        }
    }
}
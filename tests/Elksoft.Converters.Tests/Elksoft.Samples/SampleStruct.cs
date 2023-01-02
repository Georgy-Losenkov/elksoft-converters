using System;
using Elksoft.Converters;

namespace Elksoft.Samples
{
    public readonly struct SampleStruct
    {
        public SampleStruct(Int16 value)
        {
            Value = value;
        }

        public Int16 Value { get; }

        public static implicit operator SampleStruct?(Int16? value)
        {
            if (value == null)
            {
                return null;
            }

            return new SampleStruct(value.GetValueOrDefault());
        }

        public static implicit operator Int16?(SampleStruct? value)
        {
            if (value == null)
            {
                return null;
            }

            return value.GetValueOrDefault().Value;
        }

        [RejectsNull]
        public static implicit operator Int32(SampleStruct? value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.GetValueOrDefault().Value;
        }

        [RejectsNull]
        public static explicit operator SampleStruct(Int32? value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var mid = value.GetValueOrDefault();

            return new SampleStruct(checked((Int16)mid));
        }

        public static explicit operator Byte(SampleStruct value)
        {
            return checked((Byte)value.Value);
        }

        public static implicit operator SampleStruct(Byte value)
        {
            return new SampleStruct(value);
        }
    }
}
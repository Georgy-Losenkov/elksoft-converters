#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class Int128ToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new Int128ToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new Int128ToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Int128, Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int128, Boolean>() {
                { Int128.MinValue, true },
                { Int64.MinValue, true },
                { Int32.MinValue, true },
                { Int16.MinValue, true },
                { SByte.MinValue, true },
                { -1, true },
                { Int128.Zero, false },
                { Int128.One, true },
                { SByte.MaxValue, true },
                { Byte.MaxValue, true },
                { Int16.MaxValue, true },
                { UInt16.MaxValue, true },
                { Int32.MaxValue, true },
                { UInt32.MaxValue, true },
                { Int64.MaxValue, true },
                { UInt64.MaxValue, true },
                { Int128.MaxValue, true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int128 input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new Int128ToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

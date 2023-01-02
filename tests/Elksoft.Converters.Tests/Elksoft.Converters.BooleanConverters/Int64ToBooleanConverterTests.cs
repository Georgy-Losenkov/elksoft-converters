using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class Int64ToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new Int64ToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new Int64ToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Int64, Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int64, Boolean>() {
                { Int64.MinValue, true },
                { Int32.MinValue, true },
                { Int16.MinValue, true },
                { SByte.MinValue, true },
                { -1L, true },
                { 0L, false },
                { 1L, true },
                { SByte.MaxValue, true },
                { Byte.MaxValue, true },
                { Int16.MaxValue, true },
                { UInt16.MaxValue, true },
                { Int32.MaxValue, true },
                { UInt32.MaxValue, true },
                { Int64.MaxValue, true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int64 input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new Int64ToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

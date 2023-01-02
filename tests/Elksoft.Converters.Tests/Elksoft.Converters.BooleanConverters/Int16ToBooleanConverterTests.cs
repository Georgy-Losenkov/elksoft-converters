using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class Int16ToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new Int16ToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new Int16ToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Int16, Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int16, Boolean>() {
                { Int16.MinValue, true },
                { SByte.MinValue, true },
                { -1, true },
                { (Int16)0, false },
                { (Int16)1, true },
                { SByte.MaxValue, true },
                { Byte.MaxValue, true },
                { Int16.MaxValue, true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int16 input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new Int16ToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

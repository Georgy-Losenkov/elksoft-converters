using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class SByteToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new SByteToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new SByteToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<SByte, Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<SByte, Boolean>() {
                { SByte.MinValue, true },
                { (SByte)(-1), true },
                { (SByte)0, false },
                { (SByte)1, true },
                { SByte.MaxValue, true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(SByte input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new SByteToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

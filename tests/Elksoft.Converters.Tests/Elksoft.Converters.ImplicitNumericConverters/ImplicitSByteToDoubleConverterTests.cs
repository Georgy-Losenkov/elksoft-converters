using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitSByteToDoubleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitSByteToDoubleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitSByteToDoubleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<SByte, Double> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<SByte, Double>() {
                { SByte.MinValue, SByte.MinValue },
                { (SByte)(-1), -1.0 },
#if NET7_0_OR_GREATER
                { (SByte)0, Double.NegativeZero },
#endif
                { (SByte)1, 1.0 },
                { SByte.MaxValue, SByte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(SByte input, Double output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitSByteToDoubleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitSByteToHalfConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitSByteToHalfConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitSByteToHalfConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<SByte, Half> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<SByte, Half>() {
                { SByte.MinValue, SByte.MinValue },
                { (SByte)(-1), Half.NegativeOne },
                { (SByte)0, Half.Zero },
                { (SByte)1, Half.One },
                { SByte.MaxValue, SByte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(SByte input, Half output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitSByteToHalfConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

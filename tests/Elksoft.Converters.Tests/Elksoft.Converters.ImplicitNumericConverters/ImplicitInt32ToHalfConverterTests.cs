#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitInt32ToHalfConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitInt32ToHalfConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitInt32ToHalfConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Int32, Half> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int32, Half>() {
                { Int32.MinValue, Half.NegativeInfinity },
                { Int16.MinValue, (Half)Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1, Half.NegativeOne },
                { 0, Half.Zero },
                { 1, Half.One },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, (Half)Int16.MaxValue },
                { UInt16.MaxValue, Half.PositiveInfinity },
                { Int32.MaxValue, Half.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int32 input, Half output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitInt32ToHalfConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

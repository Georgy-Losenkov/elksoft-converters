#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitHalfToSingleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitHalfToSingleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitHalfToSingleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Half, Single> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, Single>() {
                { Half.NaN, Single.NaN },
                { Half.NegativeInfinity, Single.NegativeInfinity },
                { Half.MinValue, (Single)Half.MinValue },
                { (Half)Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { Half.NegativeOne, -1.0f },
                { Half.NegativeZero, Single.NegativeZero },
                { Half.Zero, Single.NegativeZero },
                { Half.Epsilon, (Single)Half.Epsilon },
                { Half.One, 1.0f },
                { Half.E, (Single)Half.E },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { (Half)Int16.MaxValue, Single.Parse("32768") },
                { Half.MaxValue, (Single)Half.MaxValue },
                { Half.PositiveInfinity, Single.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, Single output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitHalfToSingleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

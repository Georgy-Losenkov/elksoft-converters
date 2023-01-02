#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitHalfToDoubleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitHalfToDoubleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitHalfToDoubleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Half, Double> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, Double>() {
                { Half.NaN, Double.NaN },
                { Half.NegativeInfinity, Double.NegativeInfinity },
                { Half.MinValue, Double.Parse("-65504") },
                { (Half)Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { Half.NegativeOne, -1.0 },
                { Half.NegativeZero, Double.NegativeZero },
                { Half.Zero, Double.NegativeZero },
                { Half.Epsilon, (Double)Half.Epsilon },
                { Half.One, 1.0 },
                { Half.E, (Double)Half.E },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { (Half)Int16.MaxValue, Double.Parse("32768") },
                { Half.MaxValue, (Double)Half.MaxValue },
                { Half.PositiveInfinity, Double.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, Double output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitHalfToDoubleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

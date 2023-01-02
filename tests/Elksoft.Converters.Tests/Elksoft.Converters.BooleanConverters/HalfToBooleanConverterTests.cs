#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class HalfToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new HalfToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new HalfToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Half, Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, Boolean>() {
                { Half.NaN, true },
                { Half.NegativeInfinity, true },
                { Half.MinValue, true },
                { (Half)Int16.MinValue, true },
                { SByte.MinValue, true },
                { Half.NegativeOne, true },
                { Half.Zero, false },
                { Half.Epsilon, true },
                { Half.One, true },
                { Half.E, true },
                { SByte.MaxValue, true },
                { Byte.MaxValue, true },
                { (Half)Int16.MaxValue, true },
                { Half.MaxValue, true },
                { Half.PositiveInfinity, true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new HalfToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

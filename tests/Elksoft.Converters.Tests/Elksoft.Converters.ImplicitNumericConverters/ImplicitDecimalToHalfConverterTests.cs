#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitDecimalToHalfConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitDecimalToHalfConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitDecimalToHalfConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Decimal, Half> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Decimal, Half>() {
                { Decimal.MinValue, Half.NegativeInfinity },
                { Int64.MinValue, Half.NegativeInfinity },
                { Int32.MinValue, Half.NegativeInfinity },
                { Int16.MinValue, (Half)Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1m, Half.NegativeOne },
                { Decimal.Zero, Half.NegativeZero },
                { Decimal.One, Half.One },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, (Half)Int16.MaxValue },
                { UInt16.MaxValue, Half.PositiveInfinity },
                { Int32.MaxValue, Half.PositiveInfinity },
                { UInt32.MaxValue, Half.PositiveInfinity },
                { Int64.MaxValue, Half.PositiveInfinity },
                { UInt64.MaxValue, Half.PositiveInfinity },
                { Decimal.MaxValue, Half.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Decimal input, Half output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitDecimalToHalfConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

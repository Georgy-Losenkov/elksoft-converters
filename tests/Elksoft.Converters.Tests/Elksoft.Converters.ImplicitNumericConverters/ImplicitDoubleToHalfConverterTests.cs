#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitDoubleToHalfConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitDoubleToHalfConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitDoubleToHalfConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Double, Half> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, Half>() {
                { Double.NaN, Half.NaN },
                { Double.NegativeInfinity, Half.NegativeInfinity },
                { Double.MinValue, Half.NegativeInfinity },
                { (Double)Int128.MinValue, Half.NegativeInfinity },
                { Int64.MinValue, Half.NegativeInfinity },
                { Int32.MinValue, Half.NegativeInfinity },
                { Int16.MinValue, (Half)Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1.0, Half.NegativeOne },
                { Double.NegativeZero, Half.NegativeZero },
                { 0.0, Half.NegativeZero },
                { Double.Epsilon, Half.NegativeZero },
                { Single.Epsilon, Half.NegativeZero },
                { (Double)Half.Epsilon, Half.Epsilon },
                { 1.0, Half.One },
                { (Double)Half.E, Half.E },
                { Single.E, Half.E },
                { Double.E, Half.E },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, (Half)Int16.MaxValue },
                { UInt16.MaxValue, Half.PositiveInfinity },
                { Int32.MaxValue, Half.PositiveInfinity },
                { UInt32.MaxValue, Half.PositiveInfinity },
                { Int64.MaxValue, Half.PositiveInfinity },
                { UInt64.MaxValue, Half.PositiveInfinity },
                { (Double)Int128.MaxValue, Half.PositiveInfinity },
                { (Double)UInt128.MaxValue, Half.PositiveInfinity },
                { (Double)Half.MaxValue, Half.MaxValue },
                { Single.MaxValue, Half.PositiveInfinity },
                { Double.MaxValue, Half.PositiveInfinity },
                { Double.PositiveInfinity, Half.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, Half output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitDoubleToHalfConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedDoubleToDecimalConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedDoubleToDecimalConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedDoubleToDecimalConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, Decimal> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, Decimal>() {
                { Int64.MinValue, Decimal.Parse("-9223372036854780000") },
                { Int32.MinValue, Int32.MinValue },
                { Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1.0, -1m },
                { 0.0, Decimal.Zero },
                { Double.Epsilon, Decimal.Zero },
                { Single.Epsilon, Decimal.Zero },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, Decimal.Parse("0.0000000596046447753906") },
#endif
                { 1.0, Decimal.One },
#if NET7_0_OR_GREATER
                { (Double)Half.E, Decimal.Parse("2.71875") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Decimal.Parse("2.71828174591064") },
#endif
#if NET7_0_OR_GREATER
                { Double.E, Decimal.Parse("2.71828182845904") },
#endif
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
                { Int32.MaxValue, Int32.MaxValue },
                { UInt32.MaxValue, UInt32.MaxValue },
                { Int64.MaxValue, Decimal.Parse("9223372036854780000") },
                { UInt64.MaxValue, Decimal.Parse("18446744073709600000") },
#if NET7_0_OR_GREATER
                { (Double)Half.MaxValue, Decimal.Parse("65504") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, Decimal output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedDoubleToDecimalConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

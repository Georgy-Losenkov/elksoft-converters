using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedDoubleToUInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedDoubleToUInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedDoubleToUInt64Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, UInt64> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, UInt64>() {
#if NET7_0_OR_GREATER
                { Double.NegativeZero, 0UL },
#endif
                { 0.0, 0UL },
                { Double.Epsilon, 0UL },
                { Single.Epsilon, 0UL },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, 0UL },
#endif
                { 1.0, 1UL },
#if NET7_0_OR_GREATER
                { (Double)Half.E, UInt64.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, UInt64.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Double.E, UInt64.Parse("2") },
#endif
                { SByte.MaxValue, (UInt64)SByte.MaxValue },
                { Byte.MaxValue, (UInt64)Byte.MaxValue },
                { Int16.MaxValue, (UInt64)Int16.MaxValue },
                { UInt16.MaxValue, (UInt64)UInt16.MaxValue },
                { Int32.MaxValue, (UInt64)Int32.MaxValue },
                { UInt32.MaxValue, (UInt64)UInt32.MaxValue },
                { Int64.MaxValue, UInt64.Parse("9223372036854775808") },
#if NET7_0_OR_GREATER
                { (Double)Half.MaxValue, UInt64.Parse("65504") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, UInt64 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedDoubleToUInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

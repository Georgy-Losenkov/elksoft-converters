using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedDoubleToByteConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedDoubleToByteConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedDoubleToByteConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, Byte> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, Byte>() {
#if NET7_0_OR_GREATER
                { Double.NegativeZero, 0 },
#endif
                { 0.0, 0 },
                { Double.Epsilon, 0 },
                { Single.Epsilon, 0 },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, 0 },
#endif
                { 1.0, 1 },
#if NET7_0_OR_GREATER
                { (Double)Half.E, Byte.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Byte.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Double.E, Byte.Parse("2") },
#endif
                { SByte.MaxValue, 127 },
                { Byte.MaxValue, Byte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, Byte output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedDoubleToByteConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

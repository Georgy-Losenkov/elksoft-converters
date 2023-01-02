using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedDoubleToInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedDoubleToInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedDoubleToInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, Int16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, Int16>() {
                { Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1.0, -1 },
#if NET7_0_OR_GREATER
                { Double.NegativeZero, (Int16)0 },
#endif
                { 0.0, (Int16)0 },
                { Double.Epsilon, (Int16)0 },
                { Single.Epsilon, (Int16)0 },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, (Int16)0 },
#endif
                { 1.0, (Int16)1 },
#if NET7_0_OR_GREATER
                { (Double)Half.E, Int16.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Int16.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Double.E, Int16.Parse("2") },
#endif
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, Int16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, Int16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedDoubleToInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

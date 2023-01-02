using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedDoubleToInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedDoubleToInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedDoubleToInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, Int32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, Int32>() {
                { Int32.MinValue, Int32.MinValue },
                { Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1.0, -1 },
                { 0.0, 0 },
                { Double.Epsilon, 0 },
                { Single.Epsilon, 0 },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, 0 },
#endif
                { 1.0, 1 },
#if NET7_0_OR_GREATER
                { (Double)Half.E, Int32.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Int32.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Double.E, Int32.Parse("2") },
#endif
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
                { Int32.MaxValue, Int32.MaxValue },
#if NET7_0_OR_GREATER
                { (Double)Half.MaxValue, Int32.Parse("65504") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, Int32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedDoubleToInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

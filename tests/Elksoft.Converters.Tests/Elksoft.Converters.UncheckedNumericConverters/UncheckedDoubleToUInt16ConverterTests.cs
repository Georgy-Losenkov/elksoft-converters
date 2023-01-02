using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedDoubleToUInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedDoubleToUInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedDoubleToUInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, UInt16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, UInt16>() {
                { 0.0, (UInt16)0 },
                { Double.Epsilon, (UInt16)0 },
                { Single.Epsilon, (UInt16)0 },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, (UInt16)0 },
#endif
                { 1.0, (UInt16)1 },
#if NET7_0_OR_GREATER
                { (Double)Half.E, UInt16.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, UInt16.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Double.E, UInt16.Parse("2") },
#endif
                { SByte.MaxValue, (UInt16)SByte.MaxValue },
                { Byte.MaxValue, (UInt16)Byte.MaxValue },
                { Int16.MaxValue, (UInt16)Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
#if NET7_0_OR_GREATER
                { (Double)Half.MaxValue, UInt16.Parse("65504") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, UInt16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedDoubleToUInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

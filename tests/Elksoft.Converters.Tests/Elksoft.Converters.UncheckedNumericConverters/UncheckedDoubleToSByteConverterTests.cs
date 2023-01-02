using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedDoubleToSByteConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedDoubleToSByteConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedDoubleToSByteConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, SByte> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, SByte>() {
                { SByte.MinValue, SByte.MinValue },
                { -1.0, (SByte)(-1) },
                { 0.0, (SByte)0 },
                { Double.Epsilon, (SByte)0 },
                { Single.Epsilon, (SByte)0 },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, (SByte)0 },
#endif
                { 1.0, (SByte)1 },
#if NET7_0_OR_GREATER
                { (Double)Half.E, SByte.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, SByte.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Double.E, SByte.Parse("2") },
#endif
                { SByte.MaxValue, SByte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, SByte output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedDoubleToSByteConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedDoubleToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedDoubleToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedDoubleToCharConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, Char>() {
#if NET7_0_OR_GREATER
                { Double.NegativeZero, '\u0000' },
#endif
                { 0.0, '\u0000' },
                { Double.Epsilon, '\u0000' },
                { Single.Epsilon, '\u0000' },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, '\u0000' },
#endif
                { 1.0, '\u0001' },
#if NET7_0_OR_GREATER
                { (Double)Half.E, Char.Parse("") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Char.Parse("") },
#endif
#if NET7_0_OR_GREATER
                { Double.E, Char.Parse("") },
#endif
                { SByte.MaxValue, (Char)SByte.MaxValue },
                { Byte.MaxValue, (Char)Byte.MaxValue },
                { Int16.MaxValue, (Char)Int16.MaxValue },
                { UInt16.MaxValue, Char.MaxValue },
#if NET7_0_OR_GREATER
                { (Double)Half.MaxValue, Char.Parse("￠") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, Char output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedDoubleToCharConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

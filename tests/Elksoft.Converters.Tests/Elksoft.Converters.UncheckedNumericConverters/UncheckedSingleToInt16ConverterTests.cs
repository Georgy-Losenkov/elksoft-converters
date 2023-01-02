using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedSingleToInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedSingleToInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedSingleToInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, Int16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, Int16>() {
                { Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1.0f, -1 },
#if NET7_0_OR_GREATER
                { Single.NegativeZero, (Int16)0 },
#endif
                { 0.0f, (Int16)0 },
#if NET7_0_OR_GREATER
                { (Single)Half.Epsilon, (Int16)0 },
#endif
                { Single.Epsilon, (Int16)0 },
                { 1.0f, (Int16)1 },
#if NET7_0_OR_GREATER
                { (Single)Half.E, Int16.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Int16.Parse("2") },
#endif
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, Int16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, Int16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedSingleToInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

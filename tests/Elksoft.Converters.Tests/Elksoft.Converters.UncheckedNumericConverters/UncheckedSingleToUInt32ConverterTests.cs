using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedSingleToUInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedSingleToUInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedSingleToUInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, UInt32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, UInt32>() {
#if NET7_0_OR_GREATER
                { Single.NegativeZero, 0U },
#endif
                { 0.0f, 0U },
#if NET7_0_OR_GREATER
                { (Single)Half.Epsilon, 0U },
#endif
                { Single.Epsilon, 0U },
                { 1.0f, 1U },
#if NET7_0_OR_GREATER
                { (Single)Half.E, UInt32.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, UInt32.Parse("2") },
#endif
                { SByte.MaxValue, (UInt32)SByte.MaxValue },
                { Byte.MaxValue, (UInt32)Byte.MaxValue },
                { Int16.MaxValue, (UInt32)Int16.MaxValue },
                { UInt16.MaxValue, (UInt32)UInt16.MaxValue },
                { Int32.MaxValue, UInt32.Parse("2147483648") },
#if NET7_0_OR_GREATER
                { (Single)Half.MaxValue, UInt32.Parse("65504") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, UInt32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedSingleToUInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

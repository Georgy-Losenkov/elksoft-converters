using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedSingleToByteConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedSingleToByteConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedSingleToByteConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, Byte> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, Byte>() {
                { 0.0f, 0 },
#if NET7_0_OR_GREATER
                { (Single)Half.Epsilon, 0 },
#endif
                { Single.Epsilon, 0 },
                { 1.0f, 1 },
#if NET7_0_OR_GREATER
                { (Single)Half.E, Byte.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Byte.Parse("2") },
#endif
                { SByte.MaxValue, 127 },
                { Byte.MaxValue, Byte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, Byte output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedSingleToByteConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

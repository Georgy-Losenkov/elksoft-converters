using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedSingleToSByteConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedSingleToSByteConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedSingleToSByteConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, SByte> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, SByte>() {
                { SByte.MinValue, SByte.MinValue },
                { -1.0f, (SByte)(-1) },
#if NET7_0_OR_GREATER
                { Single.NegativeZero, (SByte)0 },
#endif
                { 0.0f, (SByte)0 },
#if NET7_0_OR_GREATER
                { (Single)Half.Epsilon, (SByte)0 },
#endif
                { Single.Epsilon, (SByte)0 },
                { 1.0f, (SByte)1 },
#if NET7_0_OR_GREATER
                { (Single)Half.E, SByte.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, SByte.Parse("2") },
#endif
                { SByte.MaxValue, SByte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, SByte output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedSingleToSByteConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

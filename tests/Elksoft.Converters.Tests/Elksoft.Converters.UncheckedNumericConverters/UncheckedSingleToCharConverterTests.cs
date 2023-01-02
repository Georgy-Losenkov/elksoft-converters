using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedSingleToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedSingleToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedSingleToCharConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, Char>() {
#if NET7_0_OR_GREATER
                { Single.NegativeZero, '\u0000' },
#endif
                { 0.0f, '\u0000' },
#if NET7_0_OR_GREATER
                { (Single)Half.Epsilon, '\u0000' },
#endif
                { Single.Epsilon, '\u0000' },
                { 1.0f, '\u0001' },
#if NET7_0_OR_GREATER
                { (Single)Half.E, Char.Parse("") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Char.Parse("") },
#endif
                { SByte.MaxValue, (Char)SByte.MaxValue },
                { Byte.MaxValue, (Char)Byte.MaxValue },
                { Int16.MaxValue, (Char)Int16.MaxValue },
                { UInt16.MaxValue, Char.MaxValue },
#if NET7_0_OR_GREATER
                { (Single)Half.MaxValue, Char.Parse("￠") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, Char output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedSingleToCharConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

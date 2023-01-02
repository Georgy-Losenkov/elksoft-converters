using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUInt64ToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUInt64ToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUInt64ToCharConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt64, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt64, Char>() {
                { 0UL, '\u0000' },
                { 1UL, '\u0001' },
                { (UInt64)SByte.MaxValue, (Char)SByte.MaxValue },
                { (UInt64)Byte.MaxValue, (Char)Byte.MaxValue },
                { (UInt64)Int16.MaxValue, (Char)Int16.MaxValue },
                { (UInt64)UInt16.MaxValue, Char.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt64 input, Char output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUInt64ToCharConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

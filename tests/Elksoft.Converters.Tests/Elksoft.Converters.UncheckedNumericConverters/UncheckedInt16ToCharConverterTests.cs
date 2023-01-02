using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedInt16ToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedInt16ToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedInt16ToCharConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Int16, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int16, Char>() {
                { (Int16)0, '\u0000' },
                { (Int16)1, '\u0001' },
                { SByte.MaxValue, (Char)SByte.MaxValue },
                { Byte.MaxValue, (Char)Byte.MaxValue },
                { Int16.MaxValue, (Char)Int16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int16 input, Char output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedInt16ToCharConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

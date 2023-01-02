using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedUInt32ToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedUInt32ToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedUInt32ToCharConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt32, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt32, Char>() {
                { 0U, '\u0000' },
                { 1U, '\u0001' },
                { (UInt32)SByte.MaxValue, (Char)SByte.MaxValue },
                { (UInt32)Byte.MaxValue, (Char)Byte.MaxValue },
                { (UInt32)Int16.MaxValue, (Char)Int16.MaxValue },
                { (UInt32)UInt16.MaxValue, Char.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt32 input, Char output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUInt32ToCharConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<UInt32> Convert_ThrowsException_Data()
        {
            return new TheoryData<UInt32>() {
                { (UInt32)Int32.MaxValue },
                { UInt32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(UInt32 input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUInt32ToCharConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}

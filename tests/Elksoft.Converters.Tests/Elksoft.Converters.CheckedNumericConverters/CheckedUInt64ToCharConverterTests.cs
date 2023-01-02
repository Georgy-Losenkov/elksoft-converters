using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedUInt64ToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedUInt64ToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedUInt64ToCharConverter();
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

            var converter = new CheckedUInt64ToCharConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<UInt64> Convert_ThrowsException_Data()
        {
            return new TheoryData<UInt64>() {
                { (UInt64)Int32.MaxValue },
                { (UInt64)UInt32.MaxValue },
                { (UInt64)Int64.MaxValue },
                { UInt64.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(UInt64 input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUInt64ToCharConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}

using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUInt32ToUInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUInt32ToUInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUInt32ToUInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt32, UInt16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt32, UInt16>() {
                { 0U, (UInt16)0 },
                { 1U, (UInt16)1 },
                { (UInt32)SByte.MaxValue, (UInt16)SByte.MaxValue },
                { (UInt32)Byte.MaxValue, (UInt16)Byte.MaxValue },
                { (UInt32)Int16.MaxValue, (UInt16)Int16.MaxValue },
                { (UInt32)UInt16.MaxValue, UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt32 input, UInt16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUInt32ToUInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

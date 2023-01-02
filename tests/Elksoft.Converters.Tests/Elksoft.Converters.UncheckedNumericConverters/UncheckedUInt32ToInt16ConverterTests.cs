using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUInt32ToInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUInt32ToInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUInt32ToInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt32, Int16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt32, Int16>() {
                { 0U, (Int16)0 },
                { 1U, (Int16)1 },
                { (UInt32)SByte.MaxValue, SByte.MaxValue },
                { (UInt32)Byte.MaxValue, Byte.MaxValue },
                { (UInt32)Int16.MaxValue, Int16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt32 input, Int16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUInt32ToInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

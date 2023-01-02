using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUInt16ToInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUInt16ToInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUInt16ToInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt16, Int16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt16, Int16>() {
                { (UInt16)0, (Int16)0 },
                { (UInt16)1, (Int16)1 },
                { (UInt16)SByte.MaxValue, SByte.MaxValue },
                { (UInt16)Byte.MaxValue, Byte.MaxValue },
                { (UInt16)Int16.MaxValue, Int16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt16 input, Int16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUInt16ToInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

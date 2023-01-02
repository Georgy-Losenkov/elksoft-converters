using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUInt64ToInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUInt64ToInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUInt64ToInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt64, Int32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt64, Int32>() {
                { 0UL, 0 },
                { 1UL, 1 },
                { (UInt64)SByte.MaxValue, SByte.MaxValue },
                { (UInt64)Byte.MaxValue, Byte.MaxValue },
                { (UInt64)Int16.MaxValue, Int16.MaxValue },
                { (UInt64)UInt16.MaxValue, UInt16.MaxValue },
                { (UInt64)Int32.MaxValue, Int32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt64 input, Int32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUInt64ToInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedInt64ToUInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedInt64ToUInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedInt64ToUInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Int64, UInt16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int64, UInt16>() {
                { 0L, (UInt16)0 },
                { 1L, (UInt16)1 },
                { SByte.MaxValue, (UInt16)SByte.MaxValue },
                { Byte.MaxValue, (UInt16)Byte.MaxValue },
                { Int16.MaxValue, (UInt16)Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int64 input, UInt16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedInt64ToUInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedInt64ToUInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedInt64ToUInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedInt64ToUInt64Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Int64, UInt64> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int64, UInt64>() {
                { 0L, 0UL },
                { 1L, 1UL },
                { SByte.MaxValue, (UInt64)SByte.MaxValue },
                { Byte.MaxValue, (UInt64)Byte.MaxValue },
                { Int16.MaxValue, (UInt64)Int16.MaxValue },
                { UInt16.MaxValue, (UInt64)UInt16.MaxValue },
                { Int32.MaxValue, (UInt64)Int32.MaxValue },
                { UInt32.MaxValue, (UInt64)UInt32.MaxValue },
                { Int64.MaxValue, (UInt64)Int64.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int64 input, UInt64 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedInt64ToUInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

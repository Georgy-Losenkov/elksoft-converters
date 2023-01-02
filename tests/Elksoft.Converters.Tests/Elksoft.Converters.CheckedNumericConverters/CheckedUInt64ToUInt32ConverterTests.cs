using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedUInt64ToUInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedUInt64ToUInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedUInt64ToUInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt64, UInt32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt64, UInt32>() {
                { 0UL, 0U },
                { 1UL, 1U },
                { (UInt64)SByte.MaxValue, (UInt32)SByte.MaxValue },
                { (UInt64)Byte.MaxValue, (UInt32)Byte.MaxValue },
                { (UInt64)Int16.MaxValue, (UInt32)Int16.MaxValue },
                { (UInt64)UInt16.MaxValue, (UInt32)UInt16.MaxValue },
                { (UInt64)Int32.MaxValue, (UInt32)Int32.MaxValue },
                { (UInt64)UInt32.MaxValue, UInt32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt64 input, UInt32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUInt64ToUInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<UInt64> Convert_ThrowsException_Data()
        {
            return new TheoryData<UInt64>() {
                { (UInt64)Int64.MaxValue },
                { UInt64.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(UInt64 input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUInt64ToUInt32Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}

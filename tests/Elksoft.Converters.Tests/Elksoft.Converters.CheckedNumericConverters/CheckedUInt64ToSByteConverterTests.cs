using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedUInt64ToSByteConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedUInt64ToSByteConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedUInt64ToSByteConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt64, SByte> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt64, SByte>() {
                { 0UL, (SByte)0 },
                { 1UL, (SByte)1 },
                { (UInt64)SByte.MaxValue, SByte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt64 input, SByte output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUInt64ToSByteConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<UInt64> Convert_ThrowsException_Data()
        {
            return new TheoryData<UInt64>() {
                { (UInt64)Byte.MaxValue },
                { (UInt64)Int16.MaxValue },
                { (UInt64)UInt16.MaxValue },
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

            var converter = new CheckedUInt64ToSByteConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}

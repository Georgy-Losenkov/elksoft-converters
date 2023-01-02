using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt32ToUInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt32ToUInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt32ToUInt64Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt32, UInt64> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt32, UInt64>() {
                { 0U, 0UL },
                { 1U, 1UL },
                { (UInt32)SByte.MaxValue, (UInt64)SByte.MaxValue },
                { (UInt32)Byte.MaxValue, (UInt64)Byte.MaxValue },
                { (UInt32)Int16.MaxValue, (UInt64)Int16.MaxValue },
                { (UInt32)UInt16.MaxValue, (UInt64)UInt16.MaxValue },
                { (UInt32)Int32.MaxValue, (UInt64)Int32.MaxValue },
                { UInt32.MaxValue, (UInt64)UInt32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt32 input, UInt64 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt32ToUInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

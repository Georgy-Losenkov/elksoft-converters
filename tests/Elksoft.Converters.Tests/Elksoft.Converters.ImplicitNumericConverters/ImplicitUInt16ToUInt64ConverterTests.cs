using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt16ToUInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt16ToUInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt16ToUInt64Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt16, UInt64> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt16, UInt64>() {
                { (UInt16)0, 0UL },
                { (UInt16)1, 1UL },
                { (UInt16)SByte.MaxValue, (UInt64)SByte.MaxValue },
                { (UInt16)Byte.MaxValue, (UInt64)Byte.MaxValue },
                { (UInt16)Int16.MaxValue, (UInt64)Int16.MaxValue },
                { UInt16.MaxValue, (UInt64)UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt16 input, UInt64 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt16ToUInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

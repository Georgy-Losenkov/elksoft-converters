using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt16ToUInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt16ToUInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt16ToUInt32Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt16, UInt32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt16, UInt32>() {
                { (UInt16)0, 0U },
                { (UInt16)1, 1U },
                { (UInt16)SByte.MaxValue, (UInt32)SByte.MaxValue },
                { (UInt16)Byte.MaxValue, (UInt32)Byte.MaxValue },
                { (UInt16)Int16.MaxValue, (UInt32)Int16.MaxValue },
                { UInt16.MaxValue, (UInt32)UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt16 input, UInt32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt16ToUInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

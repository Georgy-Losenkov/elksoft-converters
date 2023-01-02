using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitCharToUInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitCharToUInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitCharToUInt32Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Char, UInt32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Char, UInt32>() {
                { '\u0000', 0U },
                { '\u0001', 1U },
                { (Char)SByte.MaxValue, (UInt32)SByte.MaxValue },
                { (Char)Byte.MaxValue, (UInt32)Byte.MaxValue },
                { (Char)Int16.MaxValue, (UInt32)Int16.MaxValue },
                { Char.MaxValue, (UInt32)UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Char input, UInt32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitCharToUInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

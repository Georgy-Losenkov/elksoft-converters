using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitCharToUInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitCharToUInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitCharToUInt16Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Char, UInt16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Char, UInt16>() {
                { '\u0000', (UInt16)0 },
                { '\u0001', (UInt16)1 },
                { (Char)SByte.MaxValue, (UInt16)SByte.MaxValue },
                { (Char)Byte.MaxValue, (UInt16)Byte.MaxValue },
                { (Char)Int16.MaxValue, (UInt16)Int16.MaxValue },
                { Char.MaxValue, UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Char input, UInt16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitCharToUInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

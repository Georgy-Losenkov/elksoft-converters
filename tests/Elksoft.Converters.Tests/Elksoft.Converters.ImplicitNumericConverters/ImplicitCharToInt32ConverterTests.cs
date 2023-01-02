using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitCharToInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitCharToInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitCharToInt32Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Char, Int32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Char, Int32>() {
                { '\u0000', 0 },
                { '\u0001', 1 },
                { (Char)SByte.MaxValue, SByte.MaxValue },
                { (Char)Byte.MaxValue, Byte.MaxValue },
                { (Char)Int16.MaxValue, Int16.MaxValue },
                { Char.MaxValue, UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Char input, Int32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitCharToInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

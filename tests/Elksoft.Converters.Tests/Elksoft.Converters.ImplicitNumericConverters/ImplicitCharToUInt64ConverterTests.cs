using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitCharToUInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitCharToUInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitCharToUInt64Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Char, UInt64> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Char, UInt64>() {
                { '\u0000', 0UL },
                { '\u0001', 1UL },
                { (Char)SByte.MaxValue, (UInt64)SByte.MaxValue },
                { (Char)Byte.MaxValue, (UInt64)Byte.MaxValue },
                { (Char)Int16.MaxValue, (UInt64)Int16.MaxValue },
                { Char.MaxValue, (UInt64)UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Char input, UInt64 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitCharToUInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

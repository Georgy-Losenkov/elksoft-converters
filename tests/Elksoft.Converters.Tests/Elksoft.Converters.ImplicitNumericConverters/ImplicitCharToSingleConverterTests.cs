using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitCharToSingleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitCharToSingleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitCharToSingleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Char, Single> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Char, Single>() {
                { '\u0000', 0.0f },
                { '\u0001', 1.0f },
                { (Char)SByte.MaxValue, SByte.MaxValue },
                { (Char)Byte.MaxValue, Byte.MaxValue },
                { (Char)Int16.MaxValue, Int16.MaxValue },
                { Char.MaxValue, UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Char input, Single output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitCharToSingleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt16ToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt16ToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt16ToCharConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt16, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt16, Char>() {
                { (UInt16)0, '\u0000' },
                { (UInt16)1, '\u0001' },
                { (UInt16)SByte.MaxValue, (Char)SByte.MaxValue },
                { (UInt16)Byte.MaxValue, (Char)Byte.MaxValue },
                { (UInt16)Int16.MaxValue, (Char)Int16.MaxValue },
                { UInt16.MaxValue, Char.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt16 input, Char output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt16ToCharConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

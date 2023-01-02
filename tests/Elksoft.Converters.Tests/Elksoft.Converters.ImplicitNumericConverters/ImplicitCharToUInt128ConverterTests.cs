#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitCharToUInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitCharToUInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitCharToUInt128Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Char, UInt128> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Char, UInt128>() {
                { '\u0000', UInt128.Zero },
                { '\u0001', UInt128.One },
                { (Char)SByte.MaxValue, (UInt128)SByte.MaxValue },
                { (Char)Byte.MaxValue, (UInt128)Byte.MaxValue },
                { (Char)Int16.MaxValue, (UInt128)Int16.MaxValue },
                { Char.MaxValue, (UInt128)UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Char input, UInt128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitCharToUInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

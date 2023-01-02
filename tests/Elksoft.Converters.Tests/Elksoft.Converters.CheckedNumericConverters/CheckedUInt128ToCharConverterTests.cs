#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedUInt128ToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedUInt128ToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedUInt128ToCharConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt128, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt128, Char>() {
                { UInt128.Zero, '\u0000' },
                { UInt128.One, '\u0001' },
                { (UInt128)SByte.MaxValue, (Char)SByte.MaxValue },
                { (UInt128)Byte.MaxValue, (Char)Byte.MaxValue },
                { (UInt128)Int16.MaxValue, (Char)Int16.MaxValue },
                { (UInt128)UInt16.MaxValue, Char.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt128 input, Char output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUInt128ToCharConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<UInt128> Convert_ThrowsException_Data()
        {
            return new TheoryData<UInt128>() {
                { (UInt128)Int32.MaxValue },
                { (UInt128)UInt32.MaxValue },
                { (UInt128)Int64.MaxValue },
                { (UInt128)UInt64.MaxValue },
                { (UInt128)Int128.MaxValue },
                { UInt128.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(UInt128 input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUInt128ToCharConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif

#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedDecimalToUInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedDecimalToUInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedDecimalToUInt128Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Decimal, UInt128> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Decimal, UInt128>() {
                { Decimal.Zero, UInt128.Zero },
                { Decimal.One, UInt128.One },
                { SByte.MaxValue, (UInt128)SByte.MaxValue },
                { Byte.MaxValue, (UInt128)Byte.MaxValue },
                { Int16.MaxValue, (UInt128)Int16.MaxValue },
                { UInt16.MaxValue, (UInt128)UInt16.MaxValue },
                { Int32.MaxValue, (UInt128)Int32.MaxValue },
                { UInt32.MaxValue, (UInt128)UInt32.MaxValue },
                { Int64.MaxValue, (UInt128)Int64.MaxValue },
                { UInt64.MaxValue, (UInt128)UInt64.MaxValue },
                { Decimal.MaxValue, UInt128.Parse("79228162514264337593543950335") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Decimal input, UInt128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedDecimalToUInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<Decimal> Convert_ThrowsException_Data()
        {
            return new TheoryData<Decimal>() {
                { Decimal.MinValue },
                { Int64.MinValue },
                { Int32.MinValue },
                { Int16.MinValue },
                { SByte.MinValue },
                { -1m },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Decimal input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedDecimalToUInt128Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif

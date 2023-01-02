#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt128ToHalfConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt128ToHalfConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt128ToHalfConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt128, Half> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt128, Half>() {
                { UInt128.Zero, Half.Zero },
                { UInt128.One, Half.One },
                { (UInt128)SByte.MaxValue, SByte.MaxValue },
                { (UInt128)Byte.MaxValue, Byte.MaxValue },
                { (UInt128)Int16.MaxValue, (Half)Int16.MaxValue },
                { (UInt128)UInt16.MaxValue, Half.PositiveInfinity },
                { (UInt128)Int32.MaxValue, Half.PositiveInfinity },
                { (UInt128)UInt32.MaxValue, Half.PositiveInfinity },
                { (UInt128)Int64.MaxValue, Half.PositiveInfinity },
                { (UInt128)UInt64.MaxValue, Half.PositiveInfinity },
                { (UInt128)Int128.MaxValue, Half.PositiveInfinity },
                { UInt128.MaxValue, Half.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt128 input, Half output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt128ToHalfConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

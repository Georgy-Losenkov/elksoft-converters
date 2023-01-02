#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt64ToHalfConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt64ToHalfConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt64ToHalfConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt64, Half> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt64, Half>() {
                { 0UL, Half.NegativeZero },
                { 1UL, Half.One },
                { (UInt64)SByte.MaxValue, SByte.MaxValue },
                { (UInt64)Byte.MaxValue, Byte.MaxValue },
                { (UInt64)Int16.MaxValue, (Half)Int16.MaxValue },
                { (UInt64)UInt16.MaxValue, Half.PositiveInfinity },
                { (UInt64)Int32.MaxValue, Half.PositiveInfinity },
                { (UInt64)UInt32.MaxValue, Half.PositiveInfinity },
                { (UInt64)Int64.MaxValue, Half.PositiveInfinity },
                { UInt64.MaxValue, Half.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt64 input, Half output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt64ToHalfConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

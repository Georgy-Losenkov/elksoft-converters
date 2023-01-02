#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt16ToHalfConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt16ToHalfConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt16ToHalfConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt16, Half> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt16, Half>() {
                { (UInt16)0, Half.Zero },
                { (UInt16)1, Half.One },
                { (UInt16)SByte.MaxValue, SByte.MaxValue },
                { (UInt16)Byte.MaxValue, Byte.MaxValue },
                { (UInt16)Int16.MaxValue, (Half)Int16.MaxValue },
                { UInt16.MaxValue, Half.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt16 input, Half output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt16ToHalfConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

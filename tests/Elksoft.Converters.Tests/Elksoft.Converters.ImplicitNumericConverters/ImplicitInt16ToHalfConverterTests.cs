#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitInt16ToHalfConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitInt16ToHalfConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitInt16ToHalfConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Int16, Half> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int16, Half>() {
                { Int16.MinValue, (Half)Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1, Half.NegativeOne },
                { (Int16)0, Half.Zero },
                { (Int16)1, Half.One },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, (Half)Int16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int16 input, Half output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitInt16ToHalfConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt16ToDoubleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt16ToDoubleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt16ToDoubleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt16, Double> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt16, Double>() {
                { (UInt16)0, 0.0 },
                { (UInt16)1, 1.0 },
                { (UInt16)SByte.MaxValue, SByte.MaxValue },
                { (UInt16)Byte.MaxValue, Byte.MaxValue },
                { (UInt16)Int16.MaxValue, Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt16 input, Double output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt16ToDoubleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

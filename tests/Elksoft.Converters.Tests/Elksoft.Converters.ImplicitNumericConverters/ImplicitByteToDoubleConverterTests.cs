using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitByteToDoubleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitByteToDoubleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitByteToDoubleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Byte, Double> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Byte, Double>() {
                { 0, 0.0 },
                { 1, 1.0 },
                { 127, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Byte input, Double output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitByteToDoubleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class UInt16ToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UInt16ToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new UInt16ToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt16, Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt16, Boolean>() {
                { (UInt16)0, false },
                { (UInt16)1, true },
                { (UInt16)SByte.MaxValue, true },
                { (UInt16)Byte.MaxValue, true },
                { (UInt16)Int16.MaxValue, true },
                { UInt16.MaxValue, true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt16 input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UInt16ToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

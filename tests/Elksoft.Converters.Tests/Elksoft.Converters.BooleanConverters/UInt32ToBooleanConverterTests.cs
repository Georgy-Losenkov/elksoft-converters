using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class UInt32ToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UInt32ToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new UInt32ToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt32, Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt32, Boolean>() {
                { 0U, false },
                { 1U, true },
                { (UInt32)SByte.MaxValue, true },
                { (UInt32)Byte.MaxValue, true },
                { (UInt32)Int16.MaxValue, true },
                { (UInt32)UInt16.MaxValue, true },
                { (UInt32)Int32.MaxValue, true },
                { UInt32.MaxValue, true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt32 input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UInt32ToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

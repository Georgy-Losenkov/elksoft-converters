using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class UInt64ToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UInt64ToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new UInt64ToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt64, Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt64, Boolean>() {
                { 0UL, false },
                { 1UL, true },
                { (UInt64)SByte.MaxValue, true },
                { (UInt64)Byte.MaxValue, true },
                { (UInt64)Int16.MaxValue, true },
                { (UInt64)UInt16.MaxValue, true },
                { (UInt64)Int32.MaxValue, true },
                { (UInt64)UInt32.MaxValue, true },
                { (UInt64)Int64.MaxValue, true },
                { UInt64.MaxValue, true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt64 input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UInt64ToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

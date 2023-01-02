using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class DecimalToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new DecimalToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new DecimalToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Decimal, Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Decimal, Boolean>() {
                { Decimal.MinValue, true },
                { Int64.MinValue, true },
                { Int32.MinValue, true },
                { Int16.MinValue, true },
                { SByte.MinValue, true },
                { -1m, true },
                { Decimal.Zero, false },
                { Decimal.One, true },
                { SByte.MaxValue, true },
                { Byte.MaxValue, true },
                { Int16.MaxValue, true },
                { UInt16.MaxValue, true },
                { Int32.MaxValue, true },
                { UInt32.MaxValue, true },
                { Int64.MaxValue, true },
                { UInt64.MaxValue, true },
                { Decimal.MaxValue, true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Decimal input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new DecimalToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

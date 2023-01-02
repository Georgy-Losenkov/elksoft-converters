using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitDecimalToSingleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitDecimalToSingleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitDecimalToSingleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Decimal, Single> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Decimal, Single>() {
                { Decimal.MinValue, Single.Parse("-7.9228163E+28") },
                { Int64.MinValue, Int64.MinValue },
                { Int32.MinValue, Int32.MinValue },
                { Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1m, -1.0f },
                { Decimal.Zero, 0.0f },
                { Decimal.One, 1.0f },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
                { Int32.MaxValue, Int32.MaxValue },
                { UInt32.MaxValue, UInt32.MaxValue },
                { Int64.MaxValue, Int64.MaxValue },
                { UInt64.MaxValue, UInt64.MaxValue },
                { Decimal.MaxValue, Single.Parse("7.9228163E+28") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Decimal input, Single output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitDecimalToSingleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

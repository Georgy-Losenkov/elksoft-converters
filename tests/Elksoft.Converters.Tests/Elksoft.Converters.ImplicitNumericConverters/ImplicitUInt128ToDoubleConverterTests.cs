#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt128ToDoubleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt128ToDoubleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt128ToDoubleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt128, Double> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt128, Double>() {
                { UInt128.Zero, 0.0 },
                { UInt128.One, 1.0 },
                { (UInt128)SByte.MaxValue, SByte.MaxValue },
                { (UInt128)Byte.MaxValue, Byte.MaxValue },
                { (UInt128)Int16.MaxValue, Int16.MaxValue },
                { (UInt128)UInt16.MaxValue, UInt16.MaxValue },
                { (UInt128)Int32.MaxValue, Int32.MaxValue },
                { (UInt128)UInt32.MaxValue, UInt32.MaxValue },
                { (UInt128)Int64.MaxValue, Int64.MaxValue },
                { (UInt128)UInt64.MaxValue, UInt64.MaxValue },
                { (UInt128)Int128.MaxValue, (Double)Int128.MaxValue },
                { UInt128.MaxValue, (Double)UInt128.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt128 input, Double output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt128ToDoubleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

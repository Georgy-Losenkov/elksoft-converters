#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedDoubleToUInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedDoubleToUInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedDoubleToUInt128Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, UInt128> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, UInt128>() {
                { Double.NegativeZero, UInt128.Zero },
                { 0.0, UInt128.Zero },
                { Double.Epsilon, UInt128.Zero },
                { Single.Epsilon, UInt128.Zero },
                { (Double)Half.Epsilon, UInt128.Zero },
                { 1.0, UInt128.One },
                { (Double)Half.E, UInt128.Parse("2") },
                { Single.E, UInt128.Parse("2") },
                { Double.E, UInt128.Parse("2") },
                { SByte.MaxValue, (UInt128)SByte.MaxValue },
                { Byte.MaxValue, (UInt128)Byte.MaxValue },
                { Int16.MaxValue, (UInt128)Int16.MaxValue },
                { UInt16.MaxValue, (UInt128)UInt16.MaxValue },
                { Int32.MaxValue, (UInt128)Int32.MaxValue },
                { UInt32.MaxValue, (UInt128)UInt32.MaxValue },
                { Int64.MaxValue, UInt128.Parse("9223372036854775808") },
                { UInt64.MaxValue, UInt128.Parse("18446744073709551616") },
                { (Double)Int128.MaxValue, UInt128.Parse("170141183460469231731687303715884105728") },
                { (Double)Half.MaxValue, UInt128.Parse("65504") },
                { Single.MaxValue, UInt128.Parse("340282346638528859811704183484516925440") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, UInt128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedDoubleToUInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

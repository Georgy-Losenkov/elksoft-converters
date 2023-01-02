using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class DoubleToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new DoubleToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new DoubleToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Double, Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, Boolean>() {
                { Double.NaN, true },
                { Double.NegativeInfinity, true },
                { Double.MinValue, true },
#if NET7_0_OR_GREATER
                { (Double)Int128.MinValue, true },
#endif
                { Int64.MinValue, true },
                { Int32.MinValue, true },
                { Int16.MinValue, true },
                { SByte.MinValue, true },
                { -1.0, true },
#if NET7_0_OR_GREATER
                { Double.NegativeZero, false },
#endif
                { 0.0, false },
                { Double.Epsilon, true },
                { Single.Epsilon, true },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, true },
#endif
                { 1.0, true },
#if NET7_0_OR_GREATER
                { (Double)Half.E, true },
#endif
#if NET7_0_OR_GREATER
                { Single.E, true },
#endif
#if NET7_0_OR_GREATER
                { Double.E, true },
#endif
                { SByte.MaxValue, true },
                { Byte.MaxValue, true },
                { Int16.MaxValue, true },
                { UInt16.MaxValue, true },
                { Int32.MaxValue, true },
                { UInt32.MaxValue, true },
                { Int64.MaxValue, true },
                { UInt64.MaxValue, true },
#if NET7_0_OR_GREATER
                { (Double)Int128.MaxValue, true },
#endif
#if NET7_0_OR_GREATER
                { (Double)UInt128.MaxValue, true },
#endif
#if NET7_0_OR_GREATER
                { (Double)Half.MaxValue, true },
#endif
                { Single.MaxValue, true },
                { Double.MaxValue, true },
                { Double.PositiveInfinity, true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new DoubleToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

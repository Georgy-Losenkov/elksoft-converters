using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitSingleToDoubleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitSingleToDoubleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitSingleToDoubleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Single, Double> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, Double>() {
                { Single.NaN, Double.NaN },
                { Single.NegativeInfinity, Double.NegativeInfinity },
                { Single.MinValue, Double.Parse("-3.4028234663852886E+38") },
#if NET7_0_OR_GREATER
                { (Single)Half.MinValue, Double.Parse("-65504") },
#endif
#if NET7_0_OR_GREATER
                { (Single)Int128.MinValue, (Double)Int128.MinValue },
#endif
                { Int64.MinValue, Int64.MinValue },
                { Int32.MinValue, Int32.MinValue },
                { Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1.0f, -1.0 },
#if NET7_0_OR_GREATER
                { Single.NegativeZero, Double.NegativeZero },
#endif
#if NET7_0_OR_GREATER
                { 0.0f, Double.NegativeZero },
#endif
#if NET7_0_OR_GREATER
                { (Single)Half.Epsilon, (Double)Half.Epsilon },
#endif
                { Single.Epsilon, Single.Epsilon },
                { 1.0f, 1.0 },
#if NET7_0_OR_GREATER
                { (Single)Half.E, (Double)Half.E },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Single.E },
#endif
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
                { Int32.MaxValue, Double.Parse("2147483648") },
                { UInt32.MaxValue, Double.Parse("4294967296") },
                { Int64.MaxValue, Int64.MaxValue },
                { UInt64.MaxValue, UInt64.MaxValue },
#if NET7_0_OR_GREATER
                { (Single)Int128.MaxValue, (Double)Int128.MaxValue },
#endif
#if NET7_0_OR_GREATER
                { (Single)Half.MaxValue, (Double)Half.MaxValue },
#endif
                { Single.MaxValue, Single.MaxValue },
                { Single.PositiveInfinity, Double.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, Double output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitSingleToDoubleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

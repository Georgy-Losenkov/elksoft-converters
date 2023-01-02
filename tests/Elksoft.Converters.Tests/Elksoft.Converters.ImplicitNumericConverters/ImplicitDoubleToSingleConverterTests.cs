using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitDoubleToSingleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitDoubleToSingleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitDoubleToSingleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Double, Single> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, Single>() {
                { Double.NaN, Single.NaN },
                { Double.NegativeInfinity, Single.NegativeInfinity },
                { Double.MinValue, Single.NegativeInfinity },
#if NET7_0_OR_GREATER
                { (Double)Int128.MinValue, (Single)Int128.MinValue },
#endif
                { Int64.MinValue, Int64.MinValue },
                { Int32.MinValue, Int32.MinValue },
                { Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1.0, -1.0f },
#if NET7_0_OR_GREATER
                { Double.NegativeZero, Single.NegativeZero },
#endif
#if NET7_0_OR_GREATER
                { 0.0, Single.NegativeZero },
#endif
#if NET7_0_OR_GREATER
                { Double.Epsilon, Single.NegativeZero },
#endif
                { Single.Epsilon, Single.Epsilon },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, (Single)Half.Epsilon },
#endif
                { 1.0, 1.0f },
#if NET7_0_OR_GREATER
                { (Double)Half.E, (Single)Half.E },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Single.E },
#endif
#if NET7_0_OR_GREATER
                { Double.E, Single.E },
#endif
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
                { Int32.MaxValue, Int32.MaxValue },
                { UInt32.MaxValue, UInt32.MaxValue },
                { Int64.MaxValue, Int64.MaxValue },
                { UInt64.MaxValue, UInt64.MaxValue },
#if NET7_0_OR_GREATER
                { (Double)Int128.MaxValue, (Single)Int128.MaxValue },
#endif
#if NET7_0_OR_GREATER
                { (Double)UInt128.MaxValue, Single.PositiveInfinity },
#endif
#if NET7_0_OR_GREATER
                { (Double)Half.MaxValue, (Single)Half.MaxValue },
#endif
                { Single.MaxValue, Single.MaxValue },
                { Double.MaxValue, Single.PositiveInfinity },
                { Double.PositiveInfinity, Single.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, Single output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitDoubleToSingleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

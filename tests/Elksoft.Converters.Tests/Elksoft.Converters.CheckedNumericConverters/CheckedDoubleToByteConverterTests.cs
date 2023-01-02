using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedDoubleToByteConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedDoubleToByteConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedDoubleToByteConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, Byte> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, Byte>() {
                { 0.0, 0 },
                { Double.Epsilon, 0 },
                { Single.Epsilon, 0 },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, 0 },
#endif
                { 1.0, 1 },
#if NET7_0_OR_GREATER
                { (Double)Half.E, Byte.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Byte.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Double.E, Byte.Parse("2") },
#endif
                { SByte.MaxValue, 127 },
                { Byte.MaxValue, Byte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, Byte output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedDoubleToByteConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<Double> Convert_ThrowsException_Data()
        {
            return new TheoryData<Double>() {
                { Double.NaN },
                { Double.NegativeInfinity },
                { Double.MinValue },
#if NET7_0_OR_GREATER
                { (Double)Int128.MinValue },
#endif
                { Int64.MinValue },
                { Int32.MinValue },
                { Int16.MinValue },
                { SByte.MinValue },
                { -1.0 },
                { Int16.MaxValue },
                { UInt16.MaxValue },
                { Int32.MaxValue },
                { UInt32.MaxValue },
                { Int64.MaxValue },
                { UInt64.MaxValue },
#if NET7_0_OR_GREATER
                { (Double)Int128.MaxValue },
#endif
#if NET7_0_OR_GREATER
                { (Double)UInt128.MaxValue },
#endif
#if NET7_0_OR_GREATER
                { (Double)Half.MaxValue },
#endif
                { Single.MaxValue },
                { Double.MaxValue },
                { Double.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Double input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedDoubleToByteConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}

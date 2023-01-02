using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedDoubleToSByteConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedDoubleToSByteConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedDoubleToSByteConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, SByte> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, SByte>() {
                { SByte.MinValue, SByte.MinValue },
                { -1.0, (SByte)(-1) },
#if NET7_0_OR_GREATER
                { Double.NegativeZero, (SByte)0 },
#endif
                { 0.0, (SByte)0 },
                { Double.Epsilon, (SByte)0 },
                { Single.Epsilon, (SByte)0 },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, (SByte)0 },
#endif
                { 1.0, (SByte)1 },
#if NET7_0_OR_GREATER
                { (Double)Half.E, SByte.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, SByte.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Double.E, SByte.Parse("2") },
#endif
                { SByte.MaxValue, SByte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, SByte output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedDoubleToSByteConverter();
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
                { Byte.MaxValue },
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

            var converter = new CheckedDoubleToSByteConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}

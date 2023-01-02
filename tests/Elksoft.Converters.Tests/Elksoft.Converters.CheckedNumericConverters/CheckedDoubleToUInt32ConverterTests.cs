using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedDoubleToUInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedDoubleToUInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedDoubleToUInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, UInt32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, UInt32>() {
                { 0.0, 0U },
                { Double.Epsilon, 0U },
                { Single.Epsilon, 0U },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, 0U },
#endif
                { 1.0, 1U },
#if NET7_0_OR_GREATER
                { (Double)Half.E, UInt32.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, UInt32.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Double.E, UInt32.Parse("2") },
#endif
                { SByte.MaxValue, (UInt32)SByte.MaxValue },
                { Byte.MaxValue, (UInt32)Byte.MaxValue },
                { Int16.MaxValue, (UInt32)Int16.MaxValue },
                { UInt16.MaxValue, (UInt32)UInt16.MaxValue },
                { Int32.MaxValue, (UInt32)Int32.MaxValue },
                { UInt32.MaxValue, UInt32.MaxValue },
#if NET7_0_OR_GREATER
                { (Double)Half.MaxValue, UInt32.Parse("65504") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, UInt32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedDoubleToUInt32Converter();
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
                { Int64.MaxValue },
                { UInt64.MaxValue },
#if NET7_0_OR_GREATER
                { (Double)Int128.MaxValue },
#endif
#if NET7_0_OR_GREATER
                { (Double)UInt128.MaxValue },
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

            var converter = new CheckedDoubleToUInt32Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}

using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedDoubleToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedDoubleToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedDoubleToCharConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, Char>() {
                { 0.0, '\u0000' },
                { Double.Epsilon, '\u0000' },
                { Single.Epsilon, '\u0000' },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, '\u0000' },
#endif
                { 1.0, '\u0001' },
#if NET7_0_OR_GREATER
                { (Double)Half.E, Char.Parse("") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Char.Parse("") },
#endif
#if NET7_0_OR_GREATER
                { Double.E, Char.Parse("") },
#endif
                { SByte.MaxValue, (Char)SByte.MaxValue },
                { Byte.MaxValue, (Char)Byte.MaxValue },
                { Int16.MaxValue, (Char)Int16.MaxValue },
                { UInt16.MaxValue, Char.MaxValue },
#if NET7_0_OR_GREATER
                { (Double)Half.MaxValue, Char.Parse("￠") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, Char output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedDoubleToCharConverter();
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

            var converter = new CheckedDoubleToCharConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}

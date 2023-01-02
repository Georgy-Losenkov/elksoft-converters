#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedSingleToUInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedSingleToUInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedSingleToUInt128Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, UInt128> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, UInt128>() {
                { 0.0f, UInt128.Zero },
                { (Single)Half.Epsilon, UInt128.Zero },
                { Single.Epsilon, UInt128.Zero },
                { 1.0f, UInt128.One },
                { (Single)Half.E, UInt128.Parse("2") },
                { Single.E, UInt128.Parse("2") },
                { SByte.MaxValue, (UInt128)SByte.MaxValue },
                { Byte.MaxValue, (UInt128)Byte.MaxValue },
                { Int16.MaxValue, (UInt128)Int16.MaxValue },
                { UInt16.MaxValue, (UInt128)UInt16.MaxValue },
                { Int32.MaxValue, UInt128.Parse("2147483648") },
                { UInt32.MaxValue, UInt128.Parse("4294967296") },
                { Int64.MaxValue, UInt128.Parse("9223372036854775808") },
                { UInt64.MaxValue, UInt128.Parse("18446744073709551616") },
                { (Single)Int128.MaxValue, UInt128.Parse("170141183460469231731687303715884105728") },
                { (Single)Half.MaxValue, UInt128.Parse("65504") },
                { Single.MaxValue, UInt128.Parse("340282346638528859811704183484516925440") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, UInt128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToUInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<Single> Convert_ThrowsException_Data()
        {
            return new TheoryData<Single>() {
                { Single.NaN },
                { Single.NegativeInfinity },
                { Single.MinValue },
                { (Single)Half.MinValue },
                { (Single)Int128.MinValue },
                { Int64.MinValue },
                { Int32.MinValue },
                { Int16.MinValue },
                { SByte.MinValue },
                { -1.0f },
                { Single.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Single input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToUInt128Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif

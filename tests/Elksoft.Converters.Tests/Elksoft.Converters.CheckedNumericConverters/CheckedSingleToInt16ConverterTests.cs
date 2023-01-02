using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedSingleToInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedSingleToInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedSingleToInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, Int16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, Int16>() {
                { Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1.0f, -1 },
                { 0.0f, (Int16)0 },
#if NET7_0_OR_GREATER
                { (Single)Half.Epsilon, (Int16)0 },
#endif
                { Single.Epsilon, (Int16)0 },
                { 1.0f, (Int16)1 },
#if NET7_0_OR_GREATER
                { (Single)Half.E, Int16.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Int16.Parse("2") },
#endif
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, Int16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, Int16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToInt16Converter();
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
#if NET7_0_OR_GREATER
                { (Single)Half.MinValue },
#endif
#if NET7_0_OR_GREATER
                { (Single)Int128.MinValue },
#endif
                { Int64.MinValue },
                { Int32.MinValue },
                { UInt16.MaxValue },
                { Int32.MaxValue },
                { UInt32.MaxValue },
                { Int64.MaxValue },
                { UInt64.MaxValue },
#if NET7_0_OR_GREATER
                { (Single)Int128.MaxValue },
#endif
#if NET7_0_OR_GREATER
                { (Single)Half.MaxValue },
#endif
                { Single.MaxValue },
                { Single.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Single input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToInt16Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}

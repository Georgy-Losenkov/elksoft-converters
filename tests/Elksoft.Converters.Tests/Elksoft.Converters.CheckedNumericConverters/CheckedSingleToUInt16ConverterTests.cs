using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedSingleToUInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedSingleToUInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedSingleToUInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, UInt16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, UInt16>() {
                { 0.0f, (UInt16)0 },
#if NET7_0_OR_GREATER
                { (Single)Half.Epsilon, (UInt16)0 },
#endif
                { Single.Epsilon, (UInt16)0 },
                { 1.0f, (UInt16)1 },
#if NET7_0_OR_GREATER
                { (Single)Half.E, UInt16.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, UInt16.Parse("2") },
#endif
                { SByte.MaxValue, (UInt16)SByte.MaxValue },
                { Byte.MaxValue, (UInt16)Byte.MaxValue },
                { Int16.MaxValue, (UInt16)Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
#if NET7_0_OR_GREATER
                { (Single)Half.MaxValue, UInt16.Parse("65504") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, UInt16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToUInt16Converter();
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
                { Int16.MinValue },
                { SByte.MinValue },
                { -1.0f },
                { Int32.MaxValue },
                { UInt32.MaxValue },
                { Int64.MaxValue },
                { UInt64.MaxValue },
#if NET7_0_OR_GREATER
                { (Single)Int128.MaxValue },
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

            var converter = new CheckedSingleToUInt16Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}

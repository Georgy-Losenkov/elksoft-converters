using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedSingleToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedSingleToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedSingleToCharConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, Char>() {
                { 0.0f, '\u0000' },
#if NET7_0_OR_GREATER
                { (Single)Half.Epsilon, '\u0000' },
#endif
                { Single.Epsilon, '\u0000' },
                { 1.0f, '\u0001' },
#if NET7_0_OR_GREATER
                { (Single)Half.E, Char.Parse("") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Char.Parse("") },
#endif
                { SByte.MaxValue, (Char)SByte.MaxValue },
                { Byte.MaxValue, (Char)Byte.MaxValue },
                { Int16.MaxValue, (Char)Int16.MaxValue },
                { UInt16.MaxValue, Char.MaxValue },
#if NET7_0_OR_GREATER
                { (Single)Half.MaxValue, Char.Parse("￠") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, Char output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToCharConverter();
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

            var converter = new CheckedSingleToCharConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}

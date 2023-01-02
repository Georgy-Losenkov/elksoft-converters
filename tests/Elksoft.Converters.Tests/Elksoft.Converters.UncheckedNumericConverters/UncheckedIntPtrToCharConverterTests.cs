#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedIntPtrToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedIntPtrToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedIntPtrToCharConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<IntPtr, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<IntPtr, Char>() {
                { IntPtr.Zero, '\u0000' },
                { new IntPtr(1), '\u0001' },
                { new IntPtr(SByte.MaxValue), (Char)SByte.MaxValue },
                { new IntPtr(Byte.MaxValue), (Char)Byte.MaxValue },
                { new IntPtr(Int16.MaxValue), (Char)Int16.MaxValue },
                { new IntPtr(UInt16.MaxValue), Char.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, Char output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedIntPtrToCharConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

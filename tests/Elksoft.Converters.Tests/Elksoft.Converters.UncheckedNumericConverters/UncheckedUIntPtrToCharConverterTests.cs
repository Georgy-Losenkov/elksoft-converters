#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUIntPtrToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUIntPtrToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUIntPtrToCharConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UIntPtr, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UIntPtr, Char>() {
                { UIntPtr.Zero, '\u0000' },
                { new UIntPtr(1), '\u0001' },
                { new UIntPtr(127), (Char)SByte.MaxValue },
                { new UIntPtr(Byte.MaxValue), (Char)Byte.MaxValue },
                { new UIntPtr(32767), (Char)Int16.MaxValue },
                { new UIntPtr(UInt16.MaxValue), Char.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UIntPtr input, Char output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUIntPtrToCharConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

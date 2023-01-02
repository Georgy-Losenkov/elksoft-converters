#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUIntPtrToUInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUIntPtrToUInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUIntPtrToUInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UIntPtr, UInt16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UIntPtr, UInt16>() {
                { UIntPtr.Zero, (UInt16)0 },
                { new UIntPtr(1), (UInt16)1 },
                { new UIntPtr(127), (UInt16)SByte.MaxValue },
                { new UIntPtr(Byte.MaxValue), (UInt16)Byte.MaxValue },
                { new UIntPtr(32767), (UInt16)Int16.MaxValue },
                { new UIntPtr(UInt16.MaxValue), UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UIntPtr input, UInt16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUIntPtrToUInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

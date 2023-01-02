#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedIntPtrToUInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedIntPtrToUInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedIntPtrToUInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<IntPtr, UInt16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<IntPtr, UInt16>() {
                { IntPtr.Zero, (UInt16)0 },
                { new IntPtr(1), (UInt16)1 },
                { new IntPtr(SByte.MaxValue), (UInt16)SByte.MaxValue },
                { new IntPtr(Byte.MaxValue), (UInt16)Byte.MaxValue },
                { new IntPtr(Int16.MaxValue), (UInt16)Int16.MaxValue },
                { new IntPtr(UInt16.MaxValue), UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, UInt16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedIntPtrToUInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

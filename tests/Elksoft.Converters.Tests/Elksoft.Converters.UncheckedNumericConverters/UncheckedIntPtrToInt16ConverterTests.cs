#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedIntPtrToInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedIntPtrToInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedIntPtrToInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<IntPtr, Int16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<IntPtr, Int16>() {
                { new IntPtr(Int16.MinValue), Int16.MinValue },
                { new IntPtr(SByte.MinValue), SByte.MinValue },
                { new IntPtr(-1), -1 },
                { IntPtr.Zero, (Int16)0 },
                { new IntPtr(1), (Int16)1 },
                { new IntPtr(SByte.MaxValue), SByte.MaxValue },
                { new IntPtr(Byte.MaxValue), Byte.MaxValue },
                { new IntPtr(Int16.MaxValue), Int16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, Int16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedIntPtrToInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedIntPtrToSByteConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedIntPtrToSByteConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedIntPtrToSByteConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<IntPtr, SByte> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<IntPtr, SByte>() {
                { new IntPtr(SByte.MinValue), SByte.MinValue },
                { new IntPtr(-1), (SByte)(-1) },
                { IntPtr.Zero, (SByte)0 },
                { new IntPtr(1), (SByte)1 },
                { new IntPtr(SByte.MaxValue), SByte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, SByte output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedIntPtrToSByteConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

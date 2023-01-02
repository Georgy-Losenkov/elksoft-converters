#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUIntPtrToUInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUIntPtrToUInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUIntPtrToUInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UIntPtr, UInt32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UIntPtr, UInt32>() {
                { UIntPtr.Zero, 0U },
                { new UIntPtr(1), 1U },
                { new UIntPtr(127), (UInt32)SByte.MaxValue },
                { new UIntPtr(Byte.MaxValue), (UInt32)Byte.MaxValue },
                { new UIntPtr(32767), (UInt32)Int16.MaxValue },
                { new UIntPtr(UInt16.MaxValue), (UInt32)UInt16.MaxValue },
                { new UIntPtr(Int32.MaxValue), (UInt32)Int32.MaxValue },
                { new UIntPtr(UInt32.MaxValue), UInt32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UIntPtr input, UInt32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUIntPtrToUInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUIntPtrToInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUIntPtrToInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUIntPtrToInt64Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UIntPtr, Int64> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UIntPtr, Int64>() {
                    { UIntPtr.Zero, 0L },
                    { new UIntPtr(1), 1L },
                    { new UIntPtr(127), SByte.MaxValue },
                    { new UIntPtr(Byte.MaxValue), Byte.MaxValue },
                    { new UIntPtr(32767), Int16.MaxValue },
                    { new UIntPtr(UInt16.MaxValue), UInt16.MaxValue },
                    { new UIntPtr(Int32.MaxValue), Int32.MaxValue },
                    { new UIntPtr(UInt32.MaxValue), UInt32.MaxValue },
                };
            }
            else
            {
                return new TheoryData<UIntPtr, Int64>() {
                    { UIntPtr.Zero, 0L },
                    { new UIntPtr(1), 1L },
                    { new UIntPtr(127), SByte.MaxValue },
                    { new UIntPtr(Byte.MaxValue), Byte.MaxValue },
                    { new UIntPtr(32767), Int16.MaxValue },
                    { new UIntPtr(UInt16.MaxValue), UInt16.MaxValue },
                    { new UIntPtr(Int32.MaxValue), Int32.MaxValue },
                    { new UIntPtr(UInt32.MaxValue), UInt32.MaxValue },
                    { new UIntPtr(Int64.MaxValue), Int64.MaxValue },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UIntPtr input, Int64 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUIntPtrToInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

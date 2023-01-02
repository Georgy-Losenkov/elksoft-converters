#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class UIntPtrToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UIntPtrToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new UIntPtrToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UIntPtr, Boolean> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UIntPtr, Boolean>() {
                    { UIntPtr.Zero, false },
                    { new UIntPtr(1), true },
                    { new UIntPtr(127), true },
                    { new UIntPtr(Byte.MaxValue), true },
                    { new UIntPtr(32767), true },
                    { new UIntPtr(UInt16.MaxValue), true },
                    { new UIntPtr(Int32.MaxValue), true },
                    { new UIntPtr(UInt32.MaxValue), true },
                };
            }
            else
            {
                return new TheoryData<UIntPtr, Boolean>() {
                    { UIntPtr.Zero, false },
                    { new UIntPtr(1), true },
                    { new UIntPtr(127), true },
                    { new UIntPtr(Byte.MaxValue), true },
                    { new UIntPtr(32767), true },
                    { new UIntPtr(UInt16.MaxValue), true },
                    { new UIntPtr(Int32.MaxValue), true },
                    { new UIntPtr(UInt32.MaxValue), true },
                    { new UIntPtr(Int64.MaxValue), true },
                    { new UIntPtr(UInt64.MaxValue), true },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UIntPtr input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UIntPtrToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

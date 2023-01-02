#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class IntPtrToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new IntPtrToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new IntPtrToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<IntPtr, Boolean> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<IntPtr, Boolean>() {
                    { new IntPtr(Int32.MinValue), true },
                    { new IntPtr(Int16.MinValue), true },
                    { new IntPtr(SByte.MinValue), true },
                    { new IntPtr(-1), true },
                    { IntPtr.Zero, false },
                    { new IntPtr(1), true },
                    { new IntPtr(SByte.MaxValue), true },
                    { new IntPtr(Byte.MaxValue), true },
                    { new IntPtr(Int16.MaxValue), true },
                    { new IntPtr(UInt16.MaxValue), true },
                    { new IntPtr(Int32.MaxValue), true },
                };
            }
            else
            {
                return new TheoryData<IntPtr, Boolean>() {
                    { new IntPtr(Int64.MinValue), true },
                    { new IntPtr(Int32.MinValue), true },
                    { new IntPtr(Int16.MinValue), true },
                    { new IntPtr(SByte.MinValue), true },
                    { new IntPtr(-1), true },
                    { IntPtr.Zero, false },
                    { new IntPtr(1), true },
                    { new IntPtr(SByte.MaxValue), true },
                    { new IntPtr(Byte.MaxValue), true },
                    { new IntPtr(Int16.MaxValue), true },
                    { new IntPtr(UInt16.MaxValue), true },
                    { new IntPtr(Int32.MaxValue), true },
                    { new IntPtr(UInt32.MaxValue), true },
                    { new IntPtr(Int64.MaxValue), true },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new IntPtrToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

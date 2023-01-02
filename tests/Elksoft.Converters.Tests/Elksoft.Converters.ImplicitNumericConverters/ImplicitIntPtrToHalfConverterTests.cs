#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitIntPtrToHalfConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitIntPtrToHalfConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitIntPtrToHalfConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<IntPtr, Half> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<IntPtr, Half>() {
                    { new IntPtr(Int32.MinValue), Half.NegativeInfinity },
                    { new IntPtr(Int16.MinValue), (Half)Int16.MinValue },
                    { new IntPtr(SByte.MinValue), SByte.MinValue },
                    { new IntPtr(-1), Half.NegativeOne },
                    { IntPtr.Zero, Half.Zero },
                    { new IntPtr(1), Half.One },
                    { new IntPtr(SByte.MaxValue), SByte.MaxValue },
                    { new IntPtr(Byte.MaxValue), Byte.MaxValue },
                    { new IntPtr(Int16.MaxValue), (Half)Int16.MaxValue },
                    { new IntPtr(UInt16.MaxValue), Half.PositiveInfinity },
                    { new IntPtr(Int32.MaxValue), Half.PositiveInfinity },
                };
            }
            else
            {
                return new TheoryData<IntPtr, Half>() {
                    { new IntPtr(Int64.MinValue), Half.NegativeInfinity },
                    { new IntPtr(Int32.MinValue), Half.NegativeInfinity },
                    { new IntPtr(Int16.MinValue), (Half)Int16.MinValue },
                    { new IntPtr(SByte.MinValue), SByte.MinValue },
                    { new IntPtr(-1), Half.NegativeOne },
                    { IntPtr.Zero, Half.Zero },
                    { new IntPtr(1), Half.One },
                    { new IntPtr(SByte.MaxValue), SByte.MaxValue },
                    { new IntPtr(Byte.MaxValue), Byte.MaxValue },
                    { new IntPtr(Int16.MaxValue), (Half)Int16.MaxValue },
                    { new IntPtr(UInt16.MaxValue), Half.PositiveInfinity },
                    { new IntPtr(Int32.MaxValue), Half.PositiveInfinity },
                    { new IntPtr(UInt32.MaxValue), Half.PositiveInfinity },
                    { new IntPtr(Int64.MaxValue), Half.PositiveInfinity },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, Half output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitIntPtrToHalfConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitIntPtrToDoubleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitIntPtrToDoubleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitIntPtrToDoubleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<IntPtr, Double> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<IntPtr, Double>() {
                    { new IntPtr(Int32.MinValue), Int32.MinValue },
                    { new IntPtr(Int16.MinValue), Int16.MinValue },
                    { new IntPtr(SByte.MinValue), SByte.MinValue },
                    { new IntPtr(-1), -1.0 },
                    { IntPtr.Zero, 0.0 },
                    { new IntPtr(1), 1.0 },
                    { new IntPtr(SByte.MaxValue), SByte.MaxValue },
                    { new IntPtr(Byte.MaxValue), Byte.MaxValue },
                    { new IntPtr(Int16.MaxValue), Int16.MaxValue },
                    { new IntPtr(UInt16.MaxValue), UInt16.MaxValue },
                    { new IntPtr(Int32.MaxValue), Int32.MaxValue },
                };
            }
            else
            {
                return new TheoryData<IntPtr, Double>() {
                    { new IntPtr(Int64.MinValue), Int64.MinValue },
                    { new IntPtr(Int32.MinValue), Int32.MinValue },
                    { new IntPtr(Int16.MinValue), Int16.MinValue },
                    { new IntPtr(SByte.MinValue), SByte.MinValue },
                    { new IntPtr(-1), -1.0 },
                    { IntPtr.Zero, 0.0 },
                    { new IntPtr(1), 1.0 },
                    { new IntPtr(SByte.MaxValue), SByte.MaxValue },
                    { new IntPtr(Byte.MaxValue), Byte.MaxValue },
                    { new IntPtr(Int16.MaxValue), Int16.MaxValue },
                    { new IntPtr(UInt16.MaxValue), UInt16.MaxValue },
                    { new IntPtr(Int32.MaxValue), Int32.MaxValue },
                    { new IntPtr(UInt32.MaxValue), UInt32.MaxValue },
                    { new IntPtr(Int64.MaxValue), Int64.MaxValue },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, Double output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitIntPtrToDoubleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

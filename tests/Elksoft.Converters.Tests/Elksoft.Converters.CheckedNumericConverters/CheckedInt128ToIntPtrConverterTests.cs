#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedInt128ToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedInt128ToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedInt128ToIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Int128, IntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<Int128, IntPtr>() {
                    { Int32.MinValue, new IntPtr(Int32.MinValue) },
                    { Int16.MinValue, new IntPtr(Int16.MinValue) },
                    { SByte.MinValue, new IntPtr(SByte.MinValue) },
                    { -1, new IntPtr(-1) },
                    { Int128.Zero, IntPtr.Zero },
                    { Int128.One, new IntPtr(1) },
                    { SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, new IntPtr(Int32.MaxValue) },
                };
            }
            else
            {
                return new TheoryData<Int128, IntPtr>() {
                    { Int64.MinValue, new IntPtr(Int64.MinValue) },
                    { Int32.MinValue, new IntPtr(Int32.MinValue) },
                    { Int16.MinValue, new IntPtr(Int16.MinValue) },
                    { SByte.MinValue, new IntPtr(SByte.MinValue) },
                    { -1, new IntPtr(-1) },
                    { Int128.Zero, IntPtr.Zero },
                    { Int128.One, new IntPtr(1) },
                    { SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, new IntPtr(Int32.MaxValue) },
                    { UInt32.MaxValue, new IntPtr(UInt32.MaxValue) },
                    { Int64.MaxValue, new IntPtr(Int64.MaxValue) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int128 input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedInt128ToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<Int128> Convert_ThrowsException_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<Int128>() {
                    { Int128.MinValue },
                    { Int64.MinValue },
                    { UInt32.MaxValue },
                    { Int64.MaxValue },
                    { UInt64.MaxValue },
                    { Int128.MaxValue },
                };
            }
            else
            {
                return new TheoryData<Int128>() {
                    { Int128.MinValue },
                    { UInt64.MaxValue },
                    { Int128.MaxValue },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Int128 input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedInt128ToIntPtrConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif

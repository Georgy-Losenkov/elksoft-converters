#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitInt32ToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitInt32ToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitInt32ToIntPtrConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Int32, IntPtr> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int32, IntPtr>() {
                { Int32.MinValue, new IntPtr(Int32.MinValue) },
                { Int16.MinValue, new IntPtr(Int16.MinValue) },
                { SByte.MinValue, new IntPtr(SByte.MinValue) },
                { -1, new IntPtr(-1) },
                { 0, IntPtr.Zero },
                { 1, new IntPtr(1) },
                { SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                { Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                { Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                { UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                { Int32.MaxValue, new IntPtr(Int32.MaxValue) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int32 input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitInt32ToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

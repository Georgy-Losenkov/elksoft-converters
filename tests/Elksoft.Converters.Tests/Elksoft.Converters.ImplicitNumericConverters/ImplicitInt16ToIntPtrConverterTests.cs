#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitInt16ToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitInt16ToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitInt16ToIntPtrConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Int16, IntPtr> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int16, IntPtr>() {
                { Int16.MinValue, new IntPtr(Int16.MinValue) },
                { SByte.MinValue, new IntPtr(SByte.MinValue) },
                { -1, new IntPtr(-1) },
                { (Int16)0, IntPtr.Zero },
                { (Int16)1, new IntPtr(1) },
                { SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                { Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                { Int16.MaxValue, new IntPtr(Int16.MaxValue) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int16 input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitInt16ToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

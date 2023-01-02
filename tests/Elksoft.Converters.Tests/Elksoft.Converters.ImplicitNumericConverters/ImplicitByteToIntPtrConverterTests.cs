#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitByteToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitByteToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitByteToIntPtrConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Byte, IntPtr> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Byte, IntPtr>() {
                { 0, IntPtr.Zero },
                { 1, new IntPtr(1) },
                { 127, new IntPtr(SByte.MaxValue) },
                { Byte.MaxValue, new IntPtr(Byte.MaxValue) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Byte input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitByteToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

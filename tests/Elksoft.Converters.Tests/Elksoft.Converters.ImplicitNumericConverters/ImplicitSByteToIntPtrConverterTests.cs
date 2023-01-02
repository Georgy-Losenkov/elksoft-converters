#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitSByteToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitSByteToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitSByteToIntPtrConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<SByte, IntPtr> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<SByte, IntPtr>() {
                { SByte.MinValue, new IntPtr(SByte.MinValue) },
                { (SByte)(-1), new IntPtr(-1) },
                { (SByte)0, IntPtr.Zero },
                { (SByte)1, new IntPtr(1) },
                { SByte.MaxValue, new IntPtr(SByte.MaxValue) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(SByte input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitSByteToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

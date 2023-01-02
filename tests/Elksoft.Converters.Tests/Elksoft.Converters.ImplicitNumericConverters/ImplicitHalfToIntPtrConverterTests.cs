#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitHalfToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitHalfToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitHalfToIntPtrConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Half, IntPtr> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, IntPtr>() {
                { Half.MinValue, IntPtr.Parse("-65504") },
                { (Half)Int16.MinValue, new IntPtr(Int16.MinValue) },
                { SByte.MinValue, new IntPtr(SByte.MinValue) },
                { Half.NegativeOne, new IntPtr(-1) },
                { Half.Zero, IntPtr.Zero },
                { Half.Epsilon, IntPtr.Zero },
                { Half.One, new IntPtr(1) },
                { Half.E, IntPtr.Parse("2") },
                { SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                { Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                { (Half)Int16.MaxValue, IntPtr.Parse("32768") },
                { Half.MaxValue, IntPtr.Parse("65504") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitHalfToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

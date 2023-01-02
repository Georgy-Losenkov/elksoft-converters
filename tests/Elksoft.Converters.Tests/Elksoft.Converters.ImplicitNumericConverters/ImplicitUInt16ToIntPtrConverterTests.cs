#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt16ToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt16ToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt16ToIntPtrConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt16, IntPtr> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt16, IntPtr>() {
                { (UInt16)0, IntPtr.Zero },
                { (UInt16)1, new IntPtr(1) },
                { (UInt16)SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                { (UInt16)Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                { (UInt16)Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                { UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt16 input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt16ToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

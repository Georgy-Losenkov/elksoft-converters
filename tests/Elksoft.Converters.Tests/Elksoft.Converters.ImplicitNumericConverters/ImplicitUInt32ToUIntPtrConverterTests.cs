#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt32ToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt32ToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt32ToUIntPtrConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt32, UIntPtr> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt32, UIntPtr>() {
                { 0U, UIntPtr.Zero },
                { 1U, new UIntPtr(1) },
                { (UInt32)SByte.MaxValue, new UIntPtr(127) },
                { (UInt32)Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                { (UInt32)Int16.MaxValue, new UIntPtr(32767) },
                { (UInt32)UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                { (UInt32)Int32.MaxValue, new UIntPtr(Int32.MaxValue) },
                { UInt32.MaxValue, new UIntPtr(UInt32.MaxValue) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt32 input, UIntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt32ToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

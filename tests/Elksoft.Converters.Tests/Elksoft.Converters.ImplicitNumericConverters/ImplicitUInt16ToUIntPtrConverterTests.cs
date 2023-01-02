#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt16ToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt16ToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt16ToUIntPtrConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt16, UIntPtr> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt16, UIntPtr>() {
                { (UInt16)0, UIntPtr.Zero },
                { (UInt16)1, new UIntPtr(1) },
                { (UInt16)SByte.MaxValue, new UIntPtr(127) },
                { (UInt16)Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                { (UInt16)Int16.MaxValue, new UIntPtr(32767) },
                { UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt16 input, UIntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt16ToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedHalfToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedHalfToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedHalfToUIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Half, UIntPtr> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, UIntPtr>() {
                { Half.Zero, UIntPtr.Zero },
                { Half.Epsilon, UIntPtr.Zero },
                { Half.One, new UIntPtr(1) },
                { Half.E, UIntPtr.Parse("2") },
                { SByte.MaxValue, new UIntPtr(127) },
                { Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                { (Half)Int16.MaxValue, UIntPtr.Parse("32768") },
                { Half.MaxValue, UIntPtr.Parse("65504") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, UIntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedHalfToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

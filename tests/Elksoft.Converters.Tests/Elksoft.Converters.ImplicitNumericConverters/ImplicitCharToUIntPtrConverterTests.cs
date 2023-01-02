#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitCharToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitCharToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitCharToUIntPtrConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Char, UIntPtr> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Char, UIntPtr>() {
                { '\u0000', UIntPtr.Zero },
                { '\u0001', new UIntPtr(1) },
                { (Char)SByte.MaxValue, new UIntPtr(127) },
                { (Char)Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                { (Char)Int16.MaxValue, new UIntPtr(32767) },
                { Char.MaxValue, new UIntPtr(UInt16.MaxValue) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Char input, UIntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitCharToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

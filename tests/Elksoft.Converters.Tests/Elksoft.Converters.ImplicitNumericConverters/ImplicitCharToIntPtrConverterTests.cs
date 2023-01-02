#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitCharToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitCharToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitCharToIntPtrConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Char, IntPtr> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Char, IntPtr>() {
                { '\u0000', IntPtr.Zero },
                { '\u0001', new IntPtr(1) },
                { (Char)SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                { (Char)Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                { (Char)Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                { Char.MaxValue, new IntPtr(UInt16.MaxValue) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Char input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitCharToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

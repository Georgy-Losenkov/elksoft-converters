#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class BooleanToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new BooleanToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new BooleanToIntPtrConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Boolean, IntPtr> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Boolean, IntPtr>() {
                { false, IntPtr.Zero },
                { true, new IntPtr(1) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Boolean input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new BooleanToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedInt32ToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedInt32ToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedInt32ToUIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Int32, UIntPtr> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int32, UIntPtr>() {
                { 0, UIntPtr.Zero },
                { 1, new UIntPtr(1) },
                { SByte.MaxValue, new UIntPtr(127) },
                { Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                { Int16.MaxValue, new UIntPtr(32767) },
                { UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                { Int32.MaxValue, new UIntPtr(Int32.MaxValue) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int32 input, UIntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedInt32ToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<Int32> Convert_ThrowsException_Data()
        {
            return new TheoryData<Int32>() {
                { Int32.MinValue },
                { Int16.MinValue },
                { SByte.MinValue },
                { -1 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Int32 input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedInt32ToUIntPtrConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif

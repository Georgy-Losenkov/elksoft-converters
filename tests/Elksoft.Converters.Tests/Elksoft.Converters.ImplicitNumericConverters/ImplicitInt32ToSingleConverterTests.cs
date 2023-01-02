using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitInt32ToSingleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitInt32ToSingleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitInt32ToSingleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Int32, Single> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int32, Single>() {
                { Int32.MinValue, Int32.MinValue },
                { Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1, -1.0f },
                { 0, 0.0f },
                { 1, 1.0f },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
                { Int32.MaxValue, Int32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int32 input, Single output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitInt32ToSingleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}

﻿#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedInt128ToUInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedInt128ToUInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedInt128ToUInt128Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Int128, UInt128> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int128, UInt128>() {
                { Int128.Zero, UInt128.Zero },
                { Int128.One, UInt128.One },
                { SByte.MaxValue, (UInt128)SByte.MaxValue },
                { Byte.MaxValue, (UInt128)Byte.MaxValue },
                { Int16.MaxValue, (UInt128)Int16.MaxValue },
                { UInt16.MaxValue, (UInt128)UInt16.MaxValue },
                { Int32.MaxValue, (UInt128)Int32.MaxValue },
                { UInt32.MaxValue, (UInt128)UInt32.MaxValue },
                { Int64.MaxValue, (UInt128)Int64.MaxValue },
                { UInt64.MaxValue, (UInt128)UInt64.MaxValue },
                { Int128.MaxValue, (UInt128)Int128.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int128 input, UInt128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedInt128ToUInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<Int128> Convert_ThrowsException_Data()
        {
            return new TheoryData<Int128>() {
                { Int128.MinValue },
                { Int64.MinValue },
                { Int32.MinValue },
                { Int16.MinValue },
                { SByte.MinValue },
                { -1 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Int128 input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedInt128ToUInt128Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif

﻿#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedInt128ToUInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedInt128ToUInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedInt128ToUInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Int128, UInt32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int128, UInt32>() {
                { Int128.Zero, 0U },
                { Int128.One, 1U },
                { SByte.MaxValue, (UInt32)SByte.MaxValue },
                { Byte.MaxValue, (UInt32)Byte.MaxValue },
                { Int16.MaxValue, (UInt32)Int16.MaxValue },
                { UInt16.MaxValue, (UInt32)UInt16.MaxValue },
                { Int32.MaxValue, (UInt32)Int32.MaxValue },
                { UInt32.MaxValue, UInt32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int128 input, UInt32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedInt128ToUInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif

﻿using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedInt64ToUInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedInt64ToUInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedInt64ToUInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Int64, UInt32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int64, UInt32>() {
                { 0L, 0U },
                { 1L, 1U },
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
        public static void Convert_ReturnsExpected(Int64 input, UInt32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedInt64ToUInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<Int64> Convert_ThrowsException_Data()
        {
            return new TheoryData<Int64>() {
                { Int64.MinValue },
                { Int32.MinValue },
                { Int16.MinValue },
                { SByte.MinValue },
                { -1L },
                { Int64.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Int64 input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedInt64ToUInt32Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}

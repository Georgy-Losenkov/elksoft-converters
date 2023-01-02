﻿using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedInt64ToSByteConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedInt64ToSByteConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedInt64ToSByteConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Int64, SByte> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int64, SByte>() {
                { SByte.MinValue, SByte.MinValue },
                { -1L, (SByte)(-1) },
                { 0L, (SByte)0 },
                { 1L, (SByte)1 },
                { SByte.MaxValue, SByte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int64 input, SByte output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedInt64ToSByteConverter();
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
                { Byte.MaxValue },
                { Int16.MaxValue },
                { UInt16.MaxValue },
                { Int32.MaxValue },
                { UInt32.MaxValue },
                { Int64.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Int64 input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedInt64ToSByteConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}

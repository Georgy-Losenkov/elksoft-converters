using System;
using FluentAssertions;
using Xunit;

namespace Elksoft.Converters
{
    public class EnumDelegatesTests
    {
        public enum TestEnum : UInt64
        {
            First = 1,
            Second = 2,
            Third = 3
        }

        [Theory]
        [InlineData(TestEnum.First)]
        [InlineData(TestEnum.Second)]
        [InlineData(TestEnum.Third)]
        [InlineData((TestEnum)0)]
        [InlineData((TestEnum)125)]
        [InlineData((TestEnum)UInt64.MaxValue)]
        public static void UnderlyingToEnumDelegate_ReturnsExpected(TestEnum value)
        {
            EnumDelegates<TestEnum>.UnderlyingToEnumDelegate.Should().BeOfType<Func<UInt64, TestEnum>>()
                .Subject((UInt64)value).Should().Be(value);
        }

        [Theory]
        [InlineData(TestEnum.First)]
        [InlineData(TestEnum.Second)]
        [InlineData(TestEnum.Third)]
        [InlineData((TestEnum)0)]
        [InlineData((TestEnum)125)]
        [InlineData((TestEnum)UInt64.MaxValue)]
        public static void EnumToUnderlyingDelegate_ReturnsExpected(TestEnum value)
        {
            EnumDelegates<TestEnum>.EnumToUnderlyingDelegate.Should().BeOfType<Func<TestEnum, UInt64>>()
                .Subject(value).Should().Be((UInt64)value);
        }

        /*
        [Fact]
        public static void StringToEnumDelegate_ThrowsArgumentNullException()
        {
            new Action(() => EnumDelegates<TestEnum>.StringToEnumDelegate(null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("value");
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("true")]
        [InlineData("")]
        public static void StringToEnumDelegate_ThrowsInvalidCastException(String value)
        {
            new Action(() => EnumDelegates<TestEnum>.StringToEnumDelegate(value))
                .Should().ThrowExactly<InvalidCastException>();
        }

        [Theory]
        [InlineData("0")]
        [InlineData("First")]
        [InlineData("first")]
        [InlineData("Second")]
        [InlineData("second")]
        [InlineData("Third")]
        [InlineData("third")]
        [InlineData("125")]
        public static void StringToEnumDelegate_ReturnsExpected(String value)
        {
            EnumDelegates<TestEnum>.StringToEnumDelegate(value).Should().Be((TestEnum)Enum.Parse(typeof(TestEnum), value, true));
        }
        */
    }
}
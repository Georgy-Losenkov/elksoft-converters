using System;
using System.Collections.Generic;
using Elksoft.Samples;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StandardConverters
{
    public class StringToEnumConverterTests
    {
        [Fact]
        public static void Convert_ThrowsArgumentNullException()
        {
            var mockProvider = new Mock<IFormatProvider>();

            var converter = new StringToEnumConverter<FirstUInt32BasedEnum>();
            converter.Invoking(x => x.Convert(null, null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("value");
            converter.Invoking(x => x.Convert(null, mockProvider.Object))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("value");

            mockProvider.VerifyAll();
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("true")]
        [InlineData("")]
        public static void Convert_ThrowsInvalidCastException(String value)
        {
            var mockProvider = new Mock<IFormatProvider>();

            var converter = new StringToEnumConverter<FirstUInt32BasedEnum>();
            converter.Invoking(x => x.Convert(value, null))
                .Should().ThrowExactly<InvalidCastException>();
            converter.Invoking(x => x.Convert(value, mockProvider.Object))
                .Should().ThrowExactly<InvalidCastException>();

            mockProvider.VerifyAll();
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
        public static void Convert_ReturnsExpected(String value)
        {
            var mockProvider = new Mock<IFormatProvider>();

            var converter = new StringToEnumConverter<FirstUInt32BasedEnum>();
            converter.Convert(value, null).Should().Be((FirstUInt32BasedEnum)Enum.Parse(typeof(FirstUInt32BasedEnum), value, true));
            converter.Convert(value, mockProvider.Object).Should().Be((FirstUInt32BasedEnum)Enum.Parse(typeof(FirstUInt32BasedEnum), value, true));

            mockProvider.VerifyAll();
        }

        internal static void Verify_AcceptsNull_IsFalse<T>()
            where T : struct, Enum
        {
            var converter = new StringToEnumConverter<T>();

            converter.AcceptsNull.Should().BeFalse();
        }

        internal static void Verify_IsExplicit_IsTrue<T>()
            where T : struct, Enum
        {
            var converter = new StringToEnumConverter<T>();

            converter.IsExplicit.Should().BeTrue();
        }

        public static IEnumerable<Object[]> Types()
        {
            var types = new[] { typeof(FirstUInt32BasedEnum), typeof(FirstUInt64BasedEnum), typeof(SecondUInt32BasedEnum), typeof(SecondUInt64BasedEnum) };

            foreach (var type in types)
            {
                yield return new Object[] { type };
            }
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void AcceptsNull_IsTrue(Type type)
        {
            Utilities.Invoke(type, Verify_AcceptsNull_IsFalse<DayOfWeek>);
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void IsExplicit_IsFalse(Type type)
        {
            Utilities.Invoke(type, Verify_IsExplicit_IsTrue<DayOfWeek>);
        }
    }
}
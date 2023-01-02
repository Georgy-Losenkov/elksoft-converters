using System;
using System.Collections.Generic;
using Elksoft.Samples;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StandardConverters
{
    public class EnumToStringConverterTests
    {
        [Theory]
        [InlineData(FirstUInt32BasedEnum.First)]
        [InlineData(FirstUInt32BasedEnum.Second)]
        [InlineData(FirstUInt32BasedEnum.Third)]
        [InlineData((FirstUInt32BasedEnum)0)]
        [InlineData((FirstUInt32BasedEnum)125)]
        [InlineData((FirstUInt32BasedEnum)UInt32.MaxValue)]
        public static void Convert_ReturnsExpected(FirstUInt32BasedEnum input)
        {
            var mockProvider = new Mock<IFormatProvider>();

            var converter = new EnumToStringConverter<FirstUInt32BasedEnum>();
            converter.Convert(input, null).Should().Be(input.ToString());
            converter.Convert(input, mockProvider.Object).Should().Be(input.ToString());

            mockProvider.VerifyAll();
        }

        internal static void Verify_AcceptsNull_IsFalse<T>()
            where T : struct, Enum
        {
            var converter = new EnumToStringConverter<T>();

            converter.AcceptsNull.Should().BeFalse();
        }

        internal static void Verify_IsExplicit_IsTrue<T>()
            where T : struct, Enum
        {
            var converter = new EnumToStringConverter<T>();

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
        internal static void AcceptsNull_IsFalse(Type type)
        {
            Utilities.Invoke(type, Verify_AcceptsNull_IsFalse<DayOfWeek>);
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void IsExplicit_IsTrue(Type type)
        {
            Utilities.Invoke(type, Verify_IsExplicit_IsTrue<DayOfWeek>);
        }
    }
}
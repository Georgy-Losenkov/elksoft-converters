using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StandardConverters
{
    public class CastToNullableConverterFactoryTests
    {
        private static void Verify_Convert_ReturnsExpected<T>(T input)
            where T : struct
        {
            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CastToNullableConverter<T>();

            converter.Convert(input, null).Should().Be(input);
            converter.Convert(input, mockFormatProvider.Object).Should().Be(input);

            mockFormatProvider.VerifyAll();
        }

        [Fact]
        public static void Convert_ReturnsExpected()
        {
            Verify_Convert_ReturnsExpected(0);
            Verify_Convert_ReturnsExpected(5);
            Verify_Convert_ReturnsExpected(10);
        }

        public static IEnumerable<Object[]> Types()
        {
            var types = new[] { typeof(Int32), typeof(Double) };

            foreach (var type1 in types)
            {
                yield return new Object[] { type1 };
            }
        }

        internal static void Verify_AcceptsNull_IsTrue<T>()
            where T : struct
        {
            var converter = new CastToNullableConverter<T>();

            converter.AcceptsNull.Should().BeTrue();
        }

        internal static void Verify_IsExplicit_IsFalse<T>()
            where T : struct
        {
            var converter = new CastToNullableConverter<T>();

            converter.IsExplicit.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void AcceptsNull_IsTrue(Type type)
        {
            Utilities.Invoke(type, Verify_AcceptsNull_IsTrue<Int32>);
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void IsExplicit_IsFalse(Type type)
        {
            Utilities.Invoke(type, Verify_IsExplicit_IsFalse<Int32>);
        }
    }
}
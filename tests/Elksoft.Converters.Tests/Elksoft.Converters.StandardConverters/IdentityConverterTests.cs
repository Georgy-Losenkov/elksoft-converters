using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StandardConverters
{
    public class IdentityConverterTests
    {
        private static void Verify_Convert_ReturnsExpected<T>(T input)
        {
            var mockProvider = new Mock<IFormatProvider>();

            var converter = new IdentityConverter<T>();
            converter.Convert(input, null).Should().Be(input);
            converter.Convert(input, mockProvider.Object).Should().Be(input);

            mockProvider.VerifyAll();
        }

        [Fact]
        public static void Convert_ReturnsExpected()
        {
            Verify_Convert_ReturnsExpected(0);
            Verify_Convert_ReturnsExpected(5);
            Verify_Convert_ReturnsExpected(10);

            Verify_Convert_ReturnsExpected<Int32?>(null);
            Verify_Convert_ReturnsExpected<Int32?>(5);
            Verify_Convert_ReturnsExpected<Int32?>(10);

            Verify_Convert_ReturnsExpected<Object>(null);
            Verify_Convert_ReturnsExpected(new Object());
            Verify_Convert_ReturnsExpected(new Object());
        }

        internal static void Verify_AcceptsNull_IsTrue<T>()
        {
            var converter = new IdentityConverter<T>();

            converter.AcceptsNull.Should().BeTrue();
        }

        internal static void Verify_IsExplicit_IsFalse<T>()
        {
            var converter = new IdentityConverter<T>();

            converter.IsExplicit.Should().BeFalse();
        }

        public static IEnumerable<Object[]> Types()
        {
            var types = new[] { typeof(Int32), typeof(Int32?), typeof(String) };

            foreach (var type1 in types)
            {
                yield return new Object[] { type1 };
            }
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
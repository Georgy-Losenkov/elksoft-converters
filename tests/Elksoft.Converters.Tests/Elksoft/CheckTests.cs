using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Elksoft
{
    public class CheckTests
    {
        public static IEnumerable<String> ParameterNames()
        {
            yield return null;
            yield return "one";
            yield return "two";
            yield return "three";
        }

        public static IEnumerable<Object[]> DataFor_NotNullObject_ReturnsValue()
        {
            var values = new Object[] {
                new Object(),
                new Random(),
                "test"
            };

            foreach (var value in values)
            {
                foreach (var parameterName in ParameterNames())
                {
                    yield return new Object[] { value, parameterName };
                }
            }
        }

        public static IEnumerable<Object[]> DataFor_NotNullString_ReturnsValue()
        {
            var values = new Object[] {
                "One",
                "Two",
                "Three"
            };

            foreach (var value in values)
            {
                foreach (var parameterName in ParameterNames())
                {
                    yield return new Object[] { value, parameterName };
                }
            }
        }

        public static IEnumerable<Object[]> DataFor_Check_NullValue_ThrowsArgumentNotNullException()
        {
            foreach (var parameterName in ParameterNames())
            {
                yield return new Object[] { parameterName };
            }
        }

        [Theory]
        [MemberData(nameof(DataFor_NotNullObject_ReturnsValue))]
        internal void NotNull_Object_ReturnsValue(Object value, String parameterName)
        {
            Check.NotNull<Object>(value, parameterName).Should().BeSameAs(value);
        }

        [Theory]
        [MemberData(nameof(DataFor_NotNullString_ReturnsValue))]
        internal void NotNull_String_ReturnsValue(String value, String parameterName)
        {
            Check.NotNull<String>(value, parameterName).Should().BeSameAs(value);
        }

        [Theory]
        [MemberData(nameof(DataFor_Check_NullValue_ThrowsArgumentNotNullException))]
        internal void Check_NullObject_ThrowsArgumentNotNullException(String parameterName)
        {
            new Action(() => Check.NotNull<Object>(null, parameterName))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName(parameterName);
        }

        [Theory]
        [MemberData(nameof(DataFor_Check_NullValue_ThrowsArgumentNotNullException))]
        internal void Check_NullString_ThrowsArgumentNotNullException(String parameterName)
        {
            new Action(() => Check.NotNull<String>(null, parameterName))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName(parameterName);
        }
    }
}
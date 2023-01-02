using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StandardConverters
{
    public class ObjectDownCastConverterTests
    {
        private static void Verify_ConvertStruct_NullInput_ReturnsExpected<T>()
            where T : struct
        {
            var mockProvider = new Mock<IFormatProvider>();

            var converter = new ObjectDownCastConverter<T>();
            converter.Convert(null, null).Should().Be(default(T));
            converter.Convert(null, mockProvider.Object).Should().Be(default(T));

            mockProvider.VerifyAll();
        }


        private static void Verify_Convert_ReturnsExpected<T>(Object input)
        {
            var mockProvider = new Mock<IFormatProvider>();

            var converter = new ObjectDownCastConverter<T>();
            converter.Convert(input, null).Should().Be((T)input);
            converter.Convert(input, mockProvider.Object).Should().Be((T)input);

            mockProvider.VerifyAll();
        }

        private static void Verify_Convert_Throws_InvalidCastException<T>(Object input)
        {
            var mockProvider = new Mock<IFormatProvider>();

            var converter = new ObjectDownCastConverter<T>();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<InvalidCastException>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<InvalidCastException>();

            mockProvider.VerifyAll();
        }

        [Fact]
        public static void Convert_ReturnsExpected()
        {
            Verify_ConvertStruct_NullInput_ReturnsExpected<Int32>();
            Verify_Convert_ReturnsExpected<Int32>(0);
            Verify_Convert_ReturnsExpected<Int32>(5);
            Verify_Convert_ReturnsExpected<Int32>(10);

            Verify_ConvertStruct_NullInput_ReturnsExpected<Double>();
            Verify_Convert_ReturnsExpected<Double>(0.0);
            Verify_Convert_ReturnsExpected<Double>(5.0);
            Verify_Convert_ReturnsExpected<Double>(10.0);

            Verify_ConvertStruct_NullInput_ReturnsExpected<Decimal>();
            Verify_Convert_ReturnsExpected<Decimal>(0.0m);
            Verify_Convert_ReturnsExpected<Decimal>(5.0m);
            Verify_Convert_ReturnsExpected<Decimal>(10.0m);

            Verify_Convert_ReturnsExpected<String>(null);
            Verify_Convert_ReturnsExpected<String>("");
            Verify_Convert_ReturnsExpected<String>("something");
        }

        [Fact]
        public static void Convert_Throws_InvalidCastException()
        {
            Verify_Convert_Throws_InvalidCastException<Int32>(0.0);
            Verify_Convert_Throws_InvalidCastException<Int32>(0.0m);
            Verify_Convert_Throws_InvalidCastException<Int32>(0.0f);

            Verify_Convert_Throws_InvalidCastException<Int32?>(0.0);
            Verify_Convert_Throws_InvalidCastException<Int32?>(0.0m);
            Verify_Convert_Throws_InvalidCastException<Int32?>(0.0f);

            Verify_Convert_Throws_InvalidCastException<String>(0.0);
            Verify_Convert_Throws_InvalidCastException<String>(new Object());
            Verify_Convert_Throws_InvalidCastException<Int32?>(Array.Empty<String>());
        }

        public static IEnumerable<Object[]> Types()
        {
            var types = new[] { typeof(Int32), typeof(Double), typeof(String), typeof(Array) };

            foreach (var type1 in types)
            {
                yield return new Object[] { type1 };
            }
        }

        internal static void Verify_AcceptsNull_IsTrue<T>()
        {
            var converter = new ObjectDownCastConverter<T>();

            converter.AcceptsNull.Should().BeTrue();
        }

        internal static void Verify_IsExplicit_IsTrue<T>()
        {
            var converter = new ObjectDownCastConverter<T>();

            converter.IsExplicit.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void AcceptsNull_IsFalse(Type type)
        {
            Utilities.Invoke(type, Verify_AcceptsNull_IsTrue<Int32>);
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void IsExplicit_IsTrue(Type type)
        {
            Utilities.Invoke(type, Verify_IsExplicit_IsTrue<Int32>);
        }
    }
}
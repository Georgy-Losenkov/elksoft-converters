using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StandardConverters
{
    public class VariantConverterTests
    {
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
        internal static void Ctor_AssignsConverterFinder(Type type)
        {
            Utilities.Invoke(type, Verify_Ctor_AssignsConverterFinder<Int32>);
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void Ctor_ThrowsArgumentNullException(Type type)
        {
            Utilities.Invoke(type, Verify_Ctor_ThrowsArgumentNullException<Int32>);
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void AcceptsNull_IsTrue(Type type)
        {
            Utilities.Invoke(type, Verify_AcceptsNull_IsTrue<Int32>);
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void IsExplicit_IsTrue(Type type)
        {
            Utilities.Invoke(type, Verify_IsExplicit_IsTrue<Int32>);
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void Convert_Throws_InvalidCastException(Type type)
        {
            Utilities.Invoke(type, Verify_Convert_ThrowsInvalidCastException<Int32>);
        }

        [Theory]
        [InlineData(typeof(String))]
        internal static void Convert_Class_ReturnsNull(Type type)
        {
            Utilities.Invoke(type, Verify_Convert_Class_ReturnsNull<String>);
        }

        [Theory]
        [InlineData(typeof(Int32))]
        internal static void Convert_Struct_ReturnsDefault(Type type)
        {
            Utilities.Invoke(type, Verify_Convert_Struct_ReturnsDefault<Int32>);
        }

        [Theory]
        [InlineData(typeof(Int32))]
        internal static void Convert_NullableStruct_ReturnsNull(Type type)
        {
            Utilities.Invoke(type, Verify_Convert_NullableStruct_ReturnsNull<Int32>);
        }

        [Fact]
        public static void Convert_ReturnsExpected()
        {
            void Test(IFormatProvider formatProvider)
            {
                var mockConverter = new Mock<Converter<Double, Int32>>(MockBehavior.Strict);
                var mockConverterFinder = new Mock<IConverterFinder>(MockBehavior.Strict);
                var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

                var random = new Random();
                var input1 = random.NextDouble();
                var input2 = random.NextDouble();
                var input3 = random.NextDouble();
                var input4 = random.NextDouble();
                var output1 = random.Next();
                var output2 = random.Next();
                var message3 = "must be different";
                var message4 = "operation is invalid";

                mockConverter.Setup(x => x.Convert(input1, formatProvider)).Returns(output1);
                mockConverter.Setup(x => x.Convert(input2, formatProvider)).Returns(output2);
                mockConverter.Setup(x => x.Convert(input3, formatProvider)).Throws(new NotSupportedException(message3));
                mockConverter.Setup(x => x.Convert(input4, formatProvider)).Throws(new InvalidOperationException(message4));

                mockConverterFinder
                    .Setup(x => x.FindConverter(typeof(Double), typeof(Int32)))
                    .Returns(mockConverter.Object);

                var objectConverter = new VariantConverter<Int32>(mockConverterFinder.Object);
                objectConverter.Convert(input1, formatProvider).Should().Be(output1);
                objectConverter.Convert(input2, formatProvider).Should().Be(output2);
                objectConverter.Invoking(x => x.Convert(input3, formatProvider)).Should().ThrowExactly<NotSupportedException>().WithMessage(message3);
                objectConverter.Invoking(x => x.Convert(input4, formatProvider)).Should().ThrowExactly<InvalidOperationException>().WithMessage(message4);

                mockFormatProvider.VerifyAll();
                mockConverterFinder.VerifyAll();
                mockConverter.VerifyAll();
            }

            Test(null);

            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);
            Test(mockFormatProvider.Object);
            mockFormatProvider.VerifyAll();
        }

        private static void Verify_Ctor_AssignsConverterFinder<T>()
        {
            var mockFinder = new Mock<IConverterFinder>(MockBehavior.Strict);

            var converter = new VariantConverter<T>(mockFinder.Object);

            converter.ConverterFinder.Should().BeSameAs(mockFinder.Object);

            mockFinder.VerifyAll();
        }

        private static void Verify_Ctor_ThrowsArgumentNullException<T>()
        {
            new Func<Object>(() => new VariantConverter<T>(null)).
                Should().ThrowExactly<ArgumentNullException>().WithParameterName("converterFinder");
        }

        private static void Verify_AcceptsNull_IsTrue<T>()
        {
            var mockFinder = new Mock<IConverterFinder>(MockBehavior.Strict);

            var converter = new VariantConverter<T>(mockFinder.Object);

            converter.AcceptsNull.Should().BeTrue();

            mockFinder.VerifyAll();
        }

        private static void Verify_IsExplicit_IsTrue<T>()
        {
            var mockFinder = new Mock<IConverterFinder>(MockBehavior.Strict);

            var converter = new VariantConverter<T>(mockFinder.Object);

            converter.IsExplicit.Should().BeTrue();

            mockFinder.VerifyAll();
        }

        private static void Verify_Convert_ThrowsInvalidCastException<T>()
        {
            void Test1(IFormatProvider formatProvider)
            {
                var mockConverterFinder = new Mock<IConverterFinder>(MockBehavior.Strict);

                var objectConverter = new VariantConverter<T>(mockConverterFinder.Object);
                objectConverter.Invoking(x => x.Convert(new Object(), formatProvider))
                    .Should().ThrowExactly<InvalidCastException>();

                mockConverterFinder.VerifyAll();
            }

            void Test2(IFormatProvider formatProvider)
            {
                var mockConverterFinder = new Mock<IConverterFinder>(MockBehavior.Strict);

                mockConverterFinder
                    .Setup(x => x.FindConverter(typeof(Int32), typeof(T)))
                    .Returns<Converter<Int32, T>>(null);

                var objectConverter = new VariantConverter<T>(mockConverterFinder.Object);

                objectConverter.Invoking(x => x.Convert(12, formatProvider))
                    .Should().ThrowExactly<InvalidCastException>();

                mockConverterFinder.VerifyAll();
            }

            Test1(null);
            Test2(null);

            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);
            Test1(mockFormatProvider.Object);
            Test2(mockFormatProvider.Object);
            mockFormatProvider.VerifyAll();
        }

        private static void Verify_Convert_Class_ReturnsNull<T>()
            where T : class
        {
            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);
            var mockConverterFinder = new Mock<IConverterFinder>(MockBehavior.Strict);

            var objectConverter = new VariantConverter<T>(mockConverterFinder.Object);
            objectConverter.Convert(null, null).Should().BeNull();
            objectConverter.Convert(null, mockFormatProvider.Object).Should().BeNull();

            mockConverterFinder.VerifyAll();
            mockFormatProvider.VerifyAll();
        }

        private static void Verify_Convert_Struct_ReturnsDefault<T>()
            where T : struct
        {
            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);
            var mockConverterFinder = new Mock<IConverterFinder>(MockBehavior.Strict);

            var objectConverter = new VariantConverter<T>(mockConverterFinder.Object);
            objectConverter.Convert(null, null).Should().Be(default(T));
            objectConverter.Convert(null, mockFormatProvider.Object).Should().Be(default(T));

            mockConverterFinder.VerifyAll();
            mockFormatProvider.VerifyAll();
        }

        private static void Verify_Convert_NullableStruct_ReturnsNull<T>()
            where T : struct
        {
            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);
            var mockConverterFinder = new Mock<IConverterFinder>(MockBehavior.Strict);

            var objectConverter = new VariantConverter<T?>(mockConverterFinder.Object);
            objectConverter.Convert(null, null).Should().BeNull();
            objectConverter.Convert(null, mockFormatProvider.Object).Should().BeNull();

            mockConverterFinder.VerifyAll();
            mockFormatProvider.VerifyAll();
        }
    }
}
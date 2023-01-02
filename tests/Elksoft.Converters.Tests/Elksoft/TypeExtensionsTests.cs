using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elksoft.Samples;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft
{
    public class TypeExtensionsTests
    {
        [Fact]
        internal void AllowsNull_ThrowsArgumentNullException()
        {
            new Action(() => TypeExtensions.AllowsNull(null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("type");
        }

        [Theory]
        [InlineData(typeof(Object), true)]
        [InlineData(typeof(IFormattable), true)]
        [InlineData(typeof(DateTimeKind?), true)]
        [InlineData(typeof(Nullable<>), true)]
        [InlineData(typeof(DateTimeKind), false)]
        [InlineData(typeof(Structed<>), false)]
        internal void AllowsNull_ReturnsReference(Type type, Boolean expected)
        {
            TypeExtensions.AllowsNull(type).Should().Be(expected);
        }

        [Fact]
        internal void IsNullable_ThrowsArgumentNullException()
        {
            new Action(() => TypeExtensions.IsNullable(null, out var _))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("type");
        }

        [Theory]
        [InlineData(typeof(Byte?), typeof(Byte))]
        [InlineData(typeof(Decimal?), typeof(Decimal))]
        [InlineData(typeof(DateTimeOffset?), typeof(DateTimeOffset))]
        internal void IsNullable_NotNullableOfT_ReturnsTrue(Type type, Type baseType)
        {
            TypeExtensions.IsNullable(type, out var result).Should().BeTrue();
            result.Should().BeSameAs(baseType);
        }

        [Fact]
        internal void IsNullable_NullableOfT_ReturnsTrue()
        {
            var type = typeof(Nullable<>);
            TypeExtensions.IsNullable(type, out var result).Should().BeTrue();
            result.Should().BeSameAs(type.GetGenericArguments()[0]);
        }

        [Theory]
        [InlineData(typeof(Byte))]
        [InlineData(typeof(Object))]
        [InlineData(typeof(Enum))]
        internal void IsNullable_ReturnsFalse(Type type)
        {
            TypeExtensions.IsNullable(type, out var result).Should().BeFalse();
            result.Should().BeNull();
        }

        [Fact]
        internal void MakeNotNullable_ThrowsArgumentNullException()
        {
            new Action(() => TypeExtensions.MakeNotNullable(null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("type");
        }

        public static IEnumerable<Object[]> MakeNotNullable_ReturnsExpected_Data()
        {
            yield return new Object[] { typeof(Byte), typeof(Byte) };
            yield return new Object[] { typeof(Int16), typeof(Int16) };
            yield return new Object[] { typeof(Int32), typeof(Int32) };
            yield return new Object[] { typeof(Byte?), typeof(Byte) };
            yield return new Object[] { typeof(Int16?), typeof(Int16) };
            yield return new Object[] { typeof(Int32?), typeof(Int32) };
            yield return new Object[] { typeof(Boxed<Byte>), typeof(Boxed<Byte>) };
            yield return new Object[] { typeof(Object), typeof(Object) };
            yield return new Object[] { typeof(Boxed<>), typeof(Boxed<>) };
            yield return new Object[] { typeof(Nullable<>).MakeGenericType(typeof(Structed<>)), typeof(Structed<>) };
        }

        [Theory]
        [MemberData(nameof(MakeNotNullable_ReturnsExpected_Data))]
        internal void MakeNotNullable_NotNullableOfT_ReturnsExpected(Type type, Type expected)
        {
            TypeExtensions.MakeNotNullable(type).Should().BeSameAs(expected);
        }

        [Fact]
        internal void MakeNotNullable_NullableOfT_ReturnsExpected()
        {
            var type = typeof(Nullable<>);
            var expected = type.GetGenericArguments()[0];
            TypeExtensions.MakeNotNullable(type).Should().BeSameAs(expected);
        }

        [Fact]
        internal void MakeNullable_ThrowsArgumentNullException()
        {
            new Action(() => TypeExtensions.MakeNullable(null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("type");
        }

        public static IEnumerable<Object[]> MakeNullable_TypeIsValueType_Data()
        {
            yield return new Object[] { typeof(Byte), typeof(Byte?) };
            yield return new Object[] { typeof(Int16), typeof(Int16?) };
            yield return new Object[] { typeof(Int32), typeof(Int32?) };
            yield return new Object[] { typeof(Byte?), typeof(Byte?) };
            yield return new Object[] { typeof(Int16?), typeof(Int16?) };
            yield return new Object[] { typeof(Int32?), typeof(Int32?) };
            yield return new Object[] { typeof(Boxed<Byte>), typeof(Boxed<Byte>) };
            yield return new Object[] { typeof(Object), typeof(Object) };
            yield return new Object[] { typeof(Boxed<>), typeof(Boxed<>) };
            yield return new Object[] { typeof(Nullable<>), typeof(Nullable<>) };
            yield return new Object[] { typeof(Structed<>), typeof(Nullable<>).MakeGenericType(typeof(Structed<>)) };
        }

        [Theory]
        [MemberData(nameof(MakeNullable_TypeIsValueType_Data))]
        internal void MakeNullable_TypeIsValueType(Type type, Type expected)
        {
            TypeExtensions.MakeNullable(type).Should().BeSameAs(expected);
        }

        [Fact]
        public static void ImplementInterface_ThrowsArgumentNullException()
        {
            new Action(() => TypeExtensions.ImplementInterface(null, null))
                .Should().Throw<ArgumentNullException>().WithParameterName("type");
            new Action(() => TypeExtensions.ImplementInterface(null, typeof(ISerializable)))
                .Should().Throw<ArgumentNullException>().WithParameterName("type");
            new Action(() => TypeExtensions.ImplementInterface(typeof(Byte), null))
                .Should().Throw<ArgumentNullException>().WithParameterName("interfaceType");
        }

        [Theory]
        [InlineData(typeof(Byte), typeof(Object), false)]
        [InlineData(typeof(Byte), typeof(IComparable), true)]
        [InlineData(typeof(Byte), typeof(IComparable<Boolean>), false)]
        [InlineData(typeof(Byte[]), typeof(IEnumerable<Byte>), true)]
        [InlineData(typeof(Byte[]), typeof(IReadOnlyCollection<Byte>), true)]
        [InlineData(typeof(Byte[]), typeof(IReadOnlyList<Byte>), true)]
        [InlineData(typeof(Byte[]), typeof(IList<Byte>), true)]
        [InlineData(typeof(Byte[]), typeof(ICollection<Byte>), true)]
        [InlineData(typeof(Byte[]), typeof(IList), true)]
        [InlineData(typeof(Byte[]), typeof(ICollection), true)]
        [InlineData(typeof(IList<Byte>), typeof(ICollection<Byte>), true)]
        [InlineData(typeof(IList<Byte>), typeof(IEnumerable<Byte>), true)]
        [InlineData(typeof(IList<Byte>), typeof(IEnumerable<IComparable>), false)]
        [InlineData(typeof(IList<>), typeof(IEnumerable<>), false)]
        public static void ImplementInterface_ReturnsExpected(Type type, Type interfaceType, Boolean expected)
        {
            TypeExtensions.ImplementInterface(type, interfaceType).Should().Be(expected);
        }

        [Fact]
        public static void ImplementInterface_TypeWhichGetInterfacesReturnsNull_ReturnsExpected()
        {
            var mockType = new Mock<Type>(MockBehavior.Strict);
            mockType.Setup(t => t.GetInterfaces()).Returns((Type[])null);
            mockType.Setup(t => t.BaseType).Returns(typeof(Object));

            TypeExtensions.ImplementInterface(mockType.Object, typeof(ICloneable)).Should().Be(false);
        }

        [Fact]
        public static void ImplementInterface_TypeWhichGetInterfacesReturnsArrayWithNullElement_ReturnsExpected()
        {
            var mockType = new Mock<Type>(MockBehavior.Strict);
            mockType.Setup(t => t.GetInterfaces()).Returns(new Type[] { null });
            mockType.Setup(t => t.BaseType).Returns(typeof(Object));

            TypeExtensions.ImplementInterface(mockType.Object, typeof(ICloneable)).Should().Be(false);
        }
    }
}
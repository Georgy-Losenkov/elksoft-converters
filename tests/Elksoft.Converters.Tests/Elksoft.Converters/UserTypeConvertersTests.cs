using System;
using System.Linq;
using Elksoft.Converters.StandardConverters;
using Elksoft.Samples;
using FluentAssertions;
using Xunit;

namespace Elksoft.Converters
{
    public class UserTypeConvertersTests
    {
        [Fact]
        public static void GetExplicitConvertersFrom_ThrowsArgumentNullException()
        {
            var converters = new UserTypeConverters();

            converters.Invoking(x => x.GetExplicitConvertersFrom(null))
                .Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("inType");
        }

        [Fact]
        public static void GetImplicitConvertersFrom_ThrowsArgumentNullException()
        {
            var converters = new UserTypeConverters();

            converters.Invoking(x => x.GetImplicitConvertersFrom(null))
                .Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("inType");
        }

        [Fact]
        public static void GetExplicitConvertersTo_ThrowsArgumentNullException()
        {
            var converters = new UserTypeConverters();

            converters.Invoking(x => x.GetExplicitConvertersTo(null))
                .Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("outType");
        }

        [Fact]
        public static void GetImplicitConvertersTo_ThrowsArgumentNullException()
        {
            var converters = new UserTypeConverters();

            converters.Invoking(x => x.GetImplicitConvertersTo(null))
                .Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("outType");
        }

        [Fact]
        public static void GetExplicitConvertersFrom_SampleClass_ReturnsExpected()
        {
            var converters = new UserTypeConverters();

            var factories = converters.GetExplicitConvertersFrom(typeof(SampleClass));

            var factory1 = factories.OfType<CultureInvariantDelegateConverter<SampleClass, Byte>>().Single();
            factory1.Func.Method.DeclaringType.Should().Be(typeof(SampleClass));
            factory1.AcceptsNull.Should().BeFalse();
            factory1.IsExplicit.Should().BeTrue();
        }

        [Fact]
        public static void GetImplicitConvertersFrom_SampleClass_ReturnsExpected()
        {
            var converters = new UserTypeConverters();

            var factories = converters.GetImplicitConvertersFrom(typeof(SampleClass));

            var factory2 = factories.OfType<CultureInvariantDelegateConverter<SampleClass, Int16?>>().Single();
            factory2.Func.Method.DeclaringType.Should().Be(typeof(SampleClass));
            factory2.AcceptsNull.Should().BeTrue();
            factory2.IsExplicit.Should().BeFalse();

            var factory3 = factories.OfType<CultureInvariantDelegateConverter<SampleClass, Int32>>().Single();
            factory3.Func.Method.DeclaringType.Should().Be(typeof(SampleClass));
            factory3.AcceptsNull.Should().BeFalse();
            factory3.IsExplicit.Should().BeFalse();
        }

        [Fact]
        public static void GetExplicitConvertersTo_SampleClass_ReturnsExpected()
        {
            var converters = new UserTypeConverters();

            var factories = converters.GetExplicitConvertersTo(typeof(SampleClass));

            var factory3 = factories.OfType<CultureInvariantDelegateConverter<Int32, SampleClass>>().Single();
            factory3.Func.Method.DeclaringType.Should().Be(typeof(SampleClass));
            factory3.AcceptsNull.Should().BeFalse();
            factory3.IsExplicit.Should().BeTrue();
        }

        [Fact]
        public static void GetImplicitConvertersTo_SampleClass_ReturnsExpected()
        {
            var converters = new UserTypeConverters();

            var factories = converters.GetImplicitConvertersTo(typeof(SampleClass));

            var factory1 = factories.OfType<CultureInvariantDelegateConverter<Byte, SampleClass>>().Single();
            factory1.Func.Method.DeclaringType.Should().Be(typeof(SampleClass));
            factory1.AcceptsNull.Should().BeFalse();
            factory1.IsExplicit.Should().BeFalse();

            var factory2 = factories.OfType<CultureInvariantDelegateConverter<Int16?, SampleClass>>().Single();
            factory2.Func.Method.DeclaringType.Should().Be(typeof(SampleClass));
            factory2.AcceptsNull.Should().BeTrue();
            factory2.IsExplicit.Should().BeFalse();
        }

        [Fact]
        public static void GetExplicitConvertersFrom_SampleStruct_ReturnsExpected()
        {
            var converters = new UserTypeConverters();

            var factories = converters.GetExplicitConvertersFrom(typeof(SampleStruct));

            var factory1 = factories.OfType<CultureInvariantDelegateConverter<SampleStruct, Byte>>().Single();
            factory1.Func.Method.DeclaringType.Should().Be(typeof(SampleStruct));
            factory1.AcceptsNull.Should().BeFalse();
            factory1.IsExplicit.Should().BeTrue();
        }

        [Fact]
        public static void GetImplicitConvertersFrom_SampleStruct_ReturnsExpected()
        {
            var converters = new UserTypeConverters();

            var factories = converters.GetImplicitConvertersFrom(typeof(SampleStruct));

            var factory2 = factories.OfType<CultureInvariantDelegateConverter<SampleStruct?, Int16?>>().Single();
            factory2.Func.Method.DeclaringType.Should().Be(typeof(SampleStruct));
            factory2.AcceptsNull.Should().BeTrue();
            factory2.IsExplicit.Should().BeFalse();

            var factory3 = factories.OfType<CultureInvariantDelegateConverter<SampleStruct?, Int32>>().Single();
            factory3.Func.Method.DeclaringType.Should().Be(typeof(SampleStruct));
            factory3.AcceptsNull.Should().BeFalse();
            factory3.IsExplicit.Should().BeFalse();
        }

        [Fact]
        public static void GetExplicitConvertersTo_SampleStruct_ReturnsExpected()
        {
            var converters = new UserTypeConverters();

            var factories = converters.GetExplicitConvertersTo(typeof(SampleStruct));

            var factory3 = factories.OfType<CultureInvariantDelegateConverter<Int32?, SampleStruct>>().Single();
            factory3.Func.Method.DeclaringType.Should().Be(typeof(SampleStruct));
            factory3.AcceptsNull.Should().BeFalse();
            factory3.IsExplicit.Should().BeTrue();
        }

        [Fact]
        public static void GetImplicitConvertersTo_SampleStruct_ReturnsExpected()
        {
            var converters = new UserTypeConverters();

            var factories = converters.GetImplicitConvertersTo(typeof(SampleStruct));

            var factory1 = factories.OfType<CultureInvariantDelegateConverter<Byte, SampleStruct>>().Single();
            factory1.Func.Method.DeclaringType.Should().Be(typeof(SampleStruct));
            factory1.AcceptsNull.Should().BeFalse();
            factory1.IsExplicit.Should().BeFalse();

            var factory2 = factories.OfType<CultureInvariantDelegateConverter<Int16?, SampleStruct?>>().Single();
            factory2.Func.Method.DeclaringType.Should().Be(typeof(SampleStruct));
            factory2.AcceptsNull.Should().BeTrue();
            factory2.IsExplicit.Should().BeFalse();
        }
    }
}
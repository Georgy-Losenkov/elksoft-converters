using System;
using System.Collections.Generic;
using Elksoft.Converters.StandardConverters;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters
{
    public class VariantConvertersTests
    {
        [Fact]
        public static void GetFromObjectConverter_ThrowsArgumentNullException()
        {
            var outType = typeof(String);
            var mockConverterFinder = new Mock<IConverterFinder>(MockBehavior.Strict);

            var helper = new VariantConverters();
            new Action(() => helper.GetVariantConverter(null, mockConverterFinder.Object))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("outType");

            new Action(() => helper.GetVariantConverter(outType, null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("converterFinder");

            mockConverterFinder.VerifyAll();
        }

        private static void Verify_GetFromObjectConverter_ReturnsExpected<TOut>()
        {
            var mockConverterFinder = new Mock<IConverterFinder>(MockBehavior.Strict);

            var helper = new VariantConverters();

            helper.GetVariantConverter(typeof(TOut), mockConverterFinder.Object)
                .Should().BeOfType<VariantConverter<TOut>>()
                .Subject.ConverterFinder.Should().BeSameAs(mockConverterFinder.Object);

            mockConverterFinder.VerifyAll();
        }

        [Theory]
        [InlineData(typeof(String))]
        [InlineData(typeof(IEnumerable<String>))]
        [InlineData(typeof(Byte))]
        [InlineData(typeof(Byte?))]
        internal static void GetFromObjectConverter_ReturnsExpected(Type outType)
        {
            Utilities.Invoke(outType, Verify_GetFromObjectConverter_ReturnsExpected<Int32>);
        }
    }
}
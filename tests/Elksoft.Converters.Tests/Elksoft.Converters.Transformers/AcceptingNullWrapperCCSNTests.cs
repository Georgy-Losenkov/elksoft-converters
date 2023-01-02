using System;
using FluentAssertions;
using Moq;
using Xunit;

using ConverterClass = Elksoft.Converters.Converter<System.Object, System.Double>;
using WrapperClass = Elksoft.Converters.Transformers.AcceptingNullWrapperCCSN<System.Object, System.Double>;

namespace Elksoft.Converters.Transformers
{
    public class AcceptingNullWrapperCCSNTests
    {
        [Fact]
        internal static void Ctor_NullParameter_ThrowsArgumentNullException()
        {
            new Func<Object>(() => new WrapperClass(null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("innerConverter");
        }

        [Fact]
        internal void Ctor_AssignsInnerConverter()
        {
            var mockInnerConverter = new Mock<ConverterClass>(MockBehavior.Strict);

            var instance = new WrapperClass(mockInnerConverter.Object);
            instance.InnerConverter.Should().BeSameAs(mockInnerConverter.Object);

            mockInnerConverter.VerifyAll();
        }

        [Fact]
        internal static void AcceptsNull_IsTrue()
        {
            var mockInnerConverter = new Mock<ConverterClass>(MockBehavior.Strict);

            var instance = new WrapperClass(mockInnerConverter.Object);
            instance.AcceptsNull.Should().BeTrue();

            mockInnerConverter.VerifyAll();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        internal static void IsExplicit_IsExpected(Boolean value)
        {
            var mockInnerConverter = new Mock<ConverterClass>(MockBehavior.Strict);

            mockInnerConverter
                .Setup(x => x.IsExplicit)
                .Returns(value);

            var instance = new WrapperClass(mockInnerConverter.Object);
            instance.IsExplicit.Should().Be(value);

            mockInnerConverter.VerifyAll();
        }

        private void Verify_Convert_InvokeOnNullInput_ReturnsExpected(Double output, IFormatProvider formatProvider)
        {
            var mockInnerConverter = new Mock<ConverterClass>(MockBehavior.Strict);

            mockInnerConverter.Setup(x => x.Convert(null, formatProvider)).Returns(output);

            var instance = new WrapperClass(mockInnerConverter.Object);
            instance.Convert(null, formatProvider).Should().Be(output);

            mockInnerConverter.VerifyAll();
        }

        [Theory]
        [InlineData(-157)]
        [InlineData(124)]
        internal void Convert_InvokeOnNullInput_ReturnsExpected(Double output)
        {
            Verify_Convert_InvokeOnNullInput_ReturnsExpected(output, null);

            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);
            Verify_Convert_InvokeOnNullInput_ReturnsExpected(output, mockProvider.Object);
            mockProvider.VerifyAll();
        }

        private void Verify_Convert_InvokeOnNotNullInput_ReturnsExpected(IFormatProvider formatProvider)
        {
            var input1 = new Object();
            var input2 = new Object();
            var input3 = new Object();
            var input4 = new Object();
            var input5 = new Object();
            var input6 = new Object();
            var random = new Random();
            var output1 = random.NextDouble();
            var output2 = random.NextDouble();
            var output3 = random.NextDouble();
            var output4 = random.NextDouble();
            var message5 = "first message";
            var message6 = "second message";

            var mockInnerConverter = new Mock<ConverterClass>(MockBehavior.Strict);

            mockInnerConverter.Setup(x => x.Convert(input1, formatProvider)).Returns(output1);
            mockInnerConverter.Setup(x => x.Convert(input2, formatProvider)).Returns(output2);
            mockInnerConverter.Setup(x => x.Convert(input3, formatProvider)).Returns(output3);
            mockInnerConverter.Setup(x => x.Convert(input4, formatProvider)).Returns(output4);
            mockInnerConverter.Setup(x => x.Convert(input5, formatProvider)).Throws(() => new ArgumentException(message5));
            mockInnerConverter.Setup(x => x.Convert(input6, formatProvider)).Throws(() => new Exception(message6));

            var instance = new WrapperClass(mockInnerConverter.Object);
            instance.Convert(input1, formatProvider).Should().Be(output1);
            instance.Convert(input2, formatProvider).Should().Be(output2);
            instance.Convert(input3, formatProvider).Should().Be(output3);
            instance.Convert(input4, formatProvider).Should().Be(output4);
            instance.Invoking(x => x.Convert(input5, formatProvider)).Should().ThrowExactly<ArgumentException>().WithMessage(message5);
            instance.Invoking(x => x.Convert(input6, formatProvider)).Should().ThrowExactly<Exception>().WithMessage(message6);

            mockInnerConverter.VerifyAll();
        }

        [Fact]
        internal void Convert_InvokeOnNotNullInput_ReturnsExpected()
        {
            Verify_Convert_InvokeOnNotNullInput_ReturnsExpected(null);

            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);
            Verify_Convert_InvokeOnNotNullInput_ReturnsExpected(mockProvider.Object);
            mockProvider.VerifyAll();
        }
    }
}
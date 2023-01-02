using System;
using System.Collections.Generic;
using Elksoft.Converters.StandardConverters;
using Elksoft.Samples;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters
{
    public class CombinerTests
    {
        #region Combine
        [Fact]
        internal static void Combine_NullArguments_ThrowsArgumentNullException()
        {
            var converter = new Mock<Converter>(MockBehavior.Strict);

            var helper = new Combiner();

            helper.Invoking(x => x.Combine(null, null)).Should().ThrowExactly<ArgumentNullException>().WithParameterName("firstConverter");
            helper.Invoking(x => x.Combine(null, converter.Object)).Should().ThrowExactly<ArgumentNullException>().WithParameterName("firstConverter");
            helper.Invoking(x => x.Combine(converter.Object, null)).Should().ThrowExactly<ArgumentNullException>().WithParameterName("secondConverter");

            converter.VerifyAll();
        }

        public static IEnumerable<Object[]> Combine_IncompatibleArguments_ThrowsArgumentException_Data()
        {
            foreach (var type1 in new[] { typeof(Int16), typeof(Int16?), typeof(Boxed<Int16>) })
            {
                foreach (var type2 in new[] { typeof(Int32), typeof(Int32?), typeof(Boxed<Int32>) })
                {
                    yield return new Object[] { type1, type2 };
                }
            }
        }

        [Theory]
        [MemberData(nameof(Combine_IncompatibleArguments_ThrowsArgumentException_Data))]
        internal static void Combine_IncompatibleArguments_ThrowsArgumentException(Type firstOutType, Type secondInType)
        {
            var first = new Mock<Converter>(MockBehavior.Strict);
            first.Setup(x => x.OutType).Returns(firstOutType);

            var second = new Mock<Converter>(MockBehavior.Strict);
            second.Setup(x => x.InType).Returns(secondInType);

            var helper = new Combiner();
            helper.Invoking(x => x.Combine(first.Object, second.Object))
                .Should().ThrowExactly<ArgumentException>().WithParameterName("secondConverter");

            first.VerifyAll();
            second.VerifyAll();
        }

        public static IEnumerable<Object[]> Combine_Invoke_ReturnsExpected_Data()
        {
            foreach (var inType in new[] { typeof(Int16), typeof(Int16?), typeof(Boxed<Int16>) })
            {
                foreach (var midType in new[] { typeof(Int32), typeof(Int32?), typeof(Boxed<Int32>) })
                {
                    foreach (var outType in new[] { typeof(Int64), typeof(Int64?), typeof(Boxed<Int64>) })
                    {
                        yield return new Object[] { inType, midType, outType };
                    }
                }
            }
        }

        private static void Verify_Combine_Invoke_ReturnsExpected<TIn, TMid, TOut>()
        {
            var first = new Mock<Converter<TIn, TMid>>(MockBehavior.Strict);
            var second = new Mock<Converter<TMid, TOut>>(MockBehavior.Strict);

            var helper = new Combiner();
            var third = helper.Combine(first.Object, second.Object)
                .Should().BeOfType<CombiningConverter<TIn, TMid, TOut>>().Subject;

            third.FirstConverter.Should().BeSameAs(first.Object);
            third.SecondConverter.Should().BeSameAs(second.Object);

            first.VerifyAll();
            second.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Combine_Invoke_ReturnsExpected_Data))]
        internal static void Combine_Invoke_ReturnsExpected(Type inType, Type midType, Type outType)
        {
            Utilities.Invoke(inType, midType, outType, Verify_Combine_Invoke_ReturnsExpected<Int32, Int32, Int32>);
        }
        #endregion
    }
}
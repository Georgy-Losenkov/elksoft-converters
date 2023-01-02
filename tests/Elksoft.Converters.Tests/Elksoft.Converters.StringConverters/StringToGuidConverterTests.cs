using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class StringToGuidConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new StringToGuidConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new StringToGuidConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        [Fact]
        public static void Convert_NullInput_ThrowsArgumentNullException()
        {
            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new StringToGuidConverter();
            converter.Invoking(x => x.Convert(null, null)).Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");
            converter.Invoking(x => x.Convert(null, mockFormatProvider.Object)).Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");

            mockFormatProvider.VerifyAll();
        }

        public static TheoryData<String> Convert_NotNullInput_ReturnsExpected_Data()
        {
            return new TheoryData<String>() {
                "(cc778c0e-b308-41f6-9652-5a3f4b8395d6)",
                "{8a20791c-9c61-4191-ad24-c5acded54043}",
                "3886021a-df13-4a25-912e-92af95f7c145",
                "1f44486ba96e4dd5acb54490c92fc323",
                "c15a22f3-f756-4c1e-a0af-c6f2ad168068",
                "{ 0xa48bc3d5, 0x821e, 0x407f, { 0x95, 0xba, 0x2b, 0x4f, 0x66, 0xd5, 0xa1, 0x30 } }",
                "{0xa48bc3d5,0x821e,0x407f,{0x95,0xba,0x2b,0x4f,0x66,0xd5,0xa1,0x30}}",
            };
        }

        [Theory]
        [MemberData(nameof(Convert_NotNullInput_ReturnsExpected_Data))]
        public static void Convert_NotNullInput_ReturnsExpected(String input)
        {
            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new StringToGuidConverter();
            converter.Convert(input, null).Should().Be(Guid.Parse(input));
            converter.Convert(input, mockFormatProvider.Object).Should().Be(Guid.Parse(input));

            mockFormatProvider.VerifyAll();
        }

        public static TheoryData<String> Convert_ThrowsException_Data()
        {
            return new TheoryData<String>() {
                { String.Empty },
                { "123" },
                { "x123223" },
                { "abcd" },
                { "{123}" },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(String input)
        {
            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new StringToGuidConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockFormatProvider.Object)).Should().Throw<Exception>();

            mockFormatProvider.VerifyAll();
        }
    }
}

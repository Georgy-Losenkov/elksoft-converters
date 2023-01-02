using System;
using FluentAssertions;
using Xunit;

namespace Elksoft
{
    public class TypesTests
    {
        [Fact]
        internal void StaticPropertiesAreProperlyAssigned()
        {
            Types.Boolean.Should().BeSameAs(typeof(Boolean));
            Types.Byte.Should().BeSameAs(typeof(Byte));
            Types.Binary.Should().BeSameAs(typeof(Byte[]));
            Types.Char.Should().BeSameAs(typeof(Char));
            Types.DateOnly.Should().BeSameAs(typeof(DateOnly));
            Types.DateTime.Should().BeSameAs(typeof(DateTime));
            Types.DateTimeOffset.Should().BeSameAs(typeof(DateTimeOffset));
            Types.Decimal.Should().BeSameAs(typeof(Decimal));
            Types.Double.Should().BeSameAs(typeof(Double));
            Types.Guid.Should().BeSameAs(typeof(Guid));
            Types.Int16.Should().BeSameAs(typeof(Int16));
            Types.Int32.Should().BeSameAs(typeof(Int32));
            Types.Int64.Should().BeSameAs(typeof(Int64));
            Types.Object.Should().BeSameAs(typeof(Object));
            Types.SByte.Should().BeSameAs(typeof(SByte));
            Types.Single.Should().BeSameAs(typeof(Single));
            Types.String.Should().BeSameAs(typeof(String));
            Types.TimeOnly.Should().BeSameAs(typeof(TimeOnly));
            Types.TimeSpan.Should().BeSameAs(typeof(TimeSpan));
            Types.UInt16.Should().BeSameAs(typeof(UInt16));
            Types.UInt32.Should().BeSameAs(typeof(UInt32));
            Types.UInt64.Should().BeSameAs(typeof(UInt64));
        }
    }
}
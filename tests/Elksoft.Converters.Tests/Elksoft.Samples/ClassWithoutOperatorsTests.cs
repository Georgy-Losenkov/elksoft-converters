using System;
using FluentAssertions;
using Xunit;

namespace Elksoft.Samples
{
    public class ClassWithoutOperatorsTests
    {
        #region 0 parameters
        [Fact]
        public static void Method000_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method000()).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method001_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method001()).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method002_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method002()).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method010_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method010<Byte>()).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method011_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method011<Byte>()).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method012_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method012<Byte>()).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method100_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method100()).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method101_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method101()).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method102_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method102()).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method110_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method110<Byte>()).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method111_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method111<Byte>()).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method112_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method112<Byte>()).Should().ThrowExactly<NotImplementedException>();
        }
        #endregion

        #region 1 parameter - out
        [Fact]
        public static void Method0000_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method0000(out _)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0010_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method0010(out _)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0020_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method0020(out _)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0100_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method0100<Byte>(out _)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0110_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method0110<Byte>(out _)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0120_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method0120<Byte>(out _)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1000_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method1000(out _)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1010_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method1010(out _)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1020_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method1020(out _)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1100_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method1100<Byte>(out _)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1110_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method1110<Byte>(out _)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1120_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method1120<Byte>(out _)).Should().ThrowExactly<NotImplementedException>();
        }
        #endregion

        #region 1 parameter - ref
        [Fact]
        public static void Method0001_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            Byte value = 1;
            new Action(() => instance.Method0001(ref value)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0011_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            Byte value = 1;
            new Action(() => instance.Method0011(ref value)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0021_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            Byte value = 1;
            new Action(() => instance.Method0021(ref value)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0101_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            Byte value = 1;
            new Action(() => instance.Method0101<Byte>(ref value)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0111_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            Byte value = 1;
            new Action(() => instance.Method0111<Byte>(ref value)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0121_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            Byte value = 1;
            new Action(() => instance.Method0121<Byte>(ref value)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1001_ThrowsNotImplementedException()
        {
            Byte value = 1;
            new Action(() => ClassWithoutOperators.Method1001(ref value)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1011_ThrowsNotImplementedException()
        {
            Byte value = 1;
            new Action(() => ClassWithoutOperators.Method1011(ref value)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1021_ThrowsNotImplementedException()
        {
            Byte value = 1;
            new Action(() => ClassWithoutOperators.Method1021(ref value)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1101_ThrowsNotImplementedException()
        {
            Byte value = 1;
            new Action(() => ClassWithoutOperators.Method1101<Byte>(ref value)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1111_ThrowsNotImplementedException()
        {
            Byte value = 1;
            new Action(() => ClassWithoutOperators.Method1111<Byte>(ref value)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1121_ThrowsNotImplementedException()
        {
            Byte value = 1;
            new Action(() => ClassWithoutOperators.Method1121<Byte>(ref value)).Should().ThrowExactly<NotImplementedException>();
        }
        #endregion

        #region 1 parameter - value
        [Fact]
        public static void Method0002_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method0002(1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0012_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method0012(1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0022_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method0022(1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0102_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method0102<Byte>(1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0112_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method0112<Byte>(1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method0122_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method0122<Byte>(1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1002_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method1002(1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1012_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method1012(1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1022_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method1022(1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1102_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method1102<Byte>(1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1112_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method1112<Byte>(1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method1122_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method1122<Byte>(1)).Should().ThrowExactly<NotImplementedException>();
        }
        #endregion

        #region 2 parameters - value, value
        [Fact]
        public static void Method00022_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method00022(1, 1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method00122_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method00122(1, 1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method00222_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method00222(1, 1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method01022_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method01022<Byte>(1, 1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method01122_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method01122<Byte>(1, 1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method01222_ThrowsNotImplementedException()
        {
            var instance = new ClassWithoutOperators();
            new Action(() => instance.Method01222<Byte>(1, 1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method10022_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method10022(1, 1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method10122_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method10122(1, 1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method10222_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method10222(1, 1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method11022_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method11022<Byte>(1, 1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method11122_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method11122<Byte>(1, 1)).Should().ThrowExactly<NotImplementedException>();
        }

        [Fact]
        public static void Method11222_ThrowsNotImplementedException()
        {
            new Action(() => ClassWithoutOperators.Method11222<Byte>(1, 1)).Should().ThrowExactly<NotImplementedException>();
        }
        #endregion
    }
}
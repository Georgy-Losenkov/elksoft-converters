using System;

namespace Elksoft.Samples
{
    public readonly struct StructWithoutOperators
    {
        // Static (0/1)
        // Generic (0/1)
        // void (0) ref (1) normal (2)
        // for each parameters
        // Out parameter (0) Ref parameter (1) value parameter (2)

        #region 0 parameters
        public void Method000()
        {
            throw new NotImplementedException();
        }

        public ref Byte Method001()
        {
            throw new NotImplementedException();
        }

        public Byte Method002()
        {
            throw new NotImplementedException();
        }

        public void Method010<T>()
        {
            throw new NotImplementedException();
        }

        public ref Byte Method011<T>()
        {
            throw new NotImplementedException();
        }

        public Byte Method012<T>()
        {
            throw new NotImplementedException();
        }

        public static void Method100()
        {
            throw new NotImplementedException();
        }

        public static ref Byte Method101()
        {
            throw new NotImplementedException();
        }

        public static Byte Method102()
        {
            throw new NotImplementedException();
        }

        public static void Method110<T>()
        {
            throw new NotImplementedException();
        }

        public static ref Byte Method111<T>()
        {
            throw new NotImplementedException();
        }

        public static Byte Method112<T>()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 1 parameter - out
        public void Method0000(out Byte value)
        {
            throw new NotImplementedException();
        }

        public ref Byte Method0010(out Byte value)
        {
            throw new NotImplementedException();
        }

        public Byte Method0020(out Byte value)
        {
            throw new NotImplementedException();
        }

        public void Method0100<T>(out Byte value)
        {
            throw new NotImplementedException();
        }

        public ref Byte Method0110<T>(out Byte value)
        {
            throw new NotImplementedException();
        }

        public Byte Method0120<T>(out Byte value)
        {
            throw new NotImplementedException();
        }

        public static void Method1000(out Byte value)
        {
            throw new NotImplementedException();
        }

        public static ref Byte Method1010(out Byte value)
        {
            throw new NotImplementedException();
        }

        public static Byte Method1020(out Byte value)
        {
            throw new NotImplementedException();
        }

        public static void Method1100<T>(out Byte value)
        {
            throw new NotImplementedException();
        }

        public static ref Byte Method1110<T>(out Byte value)
        {
            throw new NotImplementedException();
        }

        public static Byte Method1120<T>(out Byte value)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 1 parameter - ref
        public void Method0001(ref Byte value)
        {
            throw new NotImplementedException();
        }

        public ref Byte Method0011(ref Byte value)
        {
            throw new NotImplementedException();
        }

        public Byte Method0021(ref Byte value)
        {
            throw new NotImplementedException();
        }

        public void Method0101<T>(ref Byte value)
        {
            throw new NotImplementedException();
        }

        public ref Byte Method0111<T>(ref Byte value)
        {
            throw new NotImplementedException();
        }

        public Byte Method0121<T>(ref Byte value)
        {
            throw new NotImplementedException();
        }

        public static void Method1001(ref Byte value)
        {
            throw new NotImplementedException();
        }

        public static ref Byte Method1011(ref Byte value)
        {
            throw new NotImplementedException();
        }

        public static Byte Method1021(ref Byte value)
        {
            throw new NotImplementedException();
        }

        public static void Method1101<T>(ref Byte value)
        {
            throw new NotImplementedException();
        }

        public static ref Byte Method1111<T>(ref Byte value)
        {
            throw new NotImplementedException();
        }

        public static Byte Method1121<T>(ref Byte value)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 1 parameter - value
        public void Method0002(Byte value)
        {
            throw new NotImplementedException();
        }

        public ref Byte Method0012(Byte value)
        {
            throw new NotImplementedException();
        }

        public Byte Method0022(Byte value)
        {
            throw new NotImplementedException();
        }

        public void Method0102<T>(Byte value)
        {
            throw new NotImplementedException();
        }

        public ref Byte Method0112<T>(Byte value)
        {
            throw new NotImplementedException();
        }

        public Byte Method0122<T>(Byte value)
        {
            throw new NotImplementedException();
        }

        public static void Method1002(Byte value)
        {
            throw new NotImplementedException();
        }

        public static ref Byte Method1012(Byte value)
        {
            throw new NotImplementedException();
        }

        public static Byte Method1022(Byte value)
        {
            throw new NotImplementedException();
        }

        public static void Method1102<T>(Byte value)
        {
            throw new NotImplementedException();
        }

        public static ref Byte Method1112<T>(Byte value)
        {
            throw new NotImplementedException();
        }

        public static Byte Method1122<T>(Byte value)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 2 parameters - value, value
        public void Method00022(Byte value1, Byte value2)
        {
            throw new NotImplementedException();
        }

        public ref Byte Method00122(Byte value1, Byte value2)
        {
            throw new NotImplementedException();
        }

        public Byte Method00222(Byte value1, Byte value2)
        {
            throw new NotImplementedException();
        }

        public void Method01022<T>(Byte value1, Byte value2)
        {
            throw new NotImplementedException();
        }

        public ref Byte Method01122<T>(Byte value1, Byte value2)
        {
            throw new NotImplementedException();
        }

        public Byte Method01222<T>(Byte value1, Byte value2)
        {
            throw new NotImplementedException();
        }

        public static void Method10022(Byte value1, Byte value2)
        {
            throw new NotImplementedException();
        }

        public static ref Byte Method10122(Byte value1, Byte value2)
        {
            throw new NotImplementedException();
        }

        public static Byte Method10222(Byte value1, Byte value2)
        {
            throw new NotImplementedException();
        }

        public static void Method11022<T>(Byte value1, Byte value2)
        {
            throw new NotImplementedException();
        }

        public static ref Byte Method11122<T>(Byte value1, Byte value2)
        {
            throw new NotImplementedException();
        }

        public static Byte Method11222<T>(Byte value1, Byte value2)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
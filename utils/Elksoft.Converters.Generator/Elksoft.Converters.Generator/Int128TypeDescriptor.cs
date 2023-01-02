namespace Elksoft.Converters.Generator
{
    internal sealed class Int128TypeDescriptor : TypeDescriptor<Int128>
    {
        public override Boolean IsSigned => true;
        public override Byte Precision => 128;
        public override NetVersion MinVersion => NetVersion.Net7;
        public override String ZeroLiteral => "Int128.Zero";
        public override String OneLiteral => "Int128.One";
        public override IEnumerable<Constant<Int128>> Constants
        {
            get
            {
                yield return new Constant<Int128>(Int128.MinValue, "Int128.MinValue");
                yield return new Constant<Int128>(Int64.MinValue, "Int64.MinValue");
                yield return new Constant<Int128>(Int32.MinValue, "Int32.MinValue");
                yield return new Constant<Int128>(Int16.MinValue, "Int16.MinValue");
                yield return new Constant<Int128>(SByte.MinValue, "SByte.MinValue");
                yield return new Constant<Int128>(-1, "-1");
                yield return new Constant<Int128>(Int128.Zero, "Int128.Zero");
                yield return new Constant<Int128>(Int128.One, "Int128.One");
                yield return new Constant<Int128>(SByte.MaxValue, "SByte.MaxValue");
                yield return new Constant<Int128>(Byte.MaxValue, "Byte.MaxValue");
                yield return new Constant<Int128>(Int16.MaxValue, "Int16.MaxValue");
                yield return new Constant<Int128>(UInt16.MaxValue, "UInt16.MaxValue");
                yield return new Constant<Int128>(Int32.MaxValue, "Int32.MaxValue");
                yield return new Constant<Int128>(UInt32.MaxValue, "UInt32.MaxValue");
                yield return new Constant<Int128>(Int64.MaxValue, "Int64.MaxValue");
                yield return new Constant<Int128>(UInt64.MaxValue, "UInt64.MaxValue");
                yield return new Constant<Int128>(Int128.MaxValue, "Int128.MaxValue");
            }
        }
    }
}
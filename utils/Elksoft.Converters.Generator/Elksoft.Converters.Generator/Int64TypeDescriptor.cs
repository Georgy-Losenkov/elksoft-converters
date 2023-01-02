namespace Elksoft.Converters.Generator
{
    internal sealed class Int64TypeDescriptor : TypeDescriptor<Int64>
    {
        public override Boolean IsSigned => true;
        public override Byte Precision => 64;
        public override String ZeroLiteral => "0L";
        public override String OneLiteral => "1L";
        public override IEnumerable<Constant<Int64>> Constants
        {
            get
            {
                yield return new Constant<Int64>(Int64.MinValue, "Int64.MinValue");
                yield return new Constant<Int64>(Int32.MinValue, "Int32.MinValue");
                yield return new Constant<Int64>(Int16.MinValue, "Int16.MinValue");
                yield return new Constant<Int64>(SByte.MinValue, "SByte.MinValue");
                yield return new Constant<Int64>(-1L, "-1L");
                yield return new Constant<Int64>(0L, "0L");
                yield return new Constant<Int64>(1L, "1L");
                yield return new Constant<Int64>(SByte.MaxValue, "SByte.MaxValue");
                yield return new Constant<Int64>(Byte.MaxValue, "Byte.MaxValue");
                yield return new Constant<Int64>(Int16.MaxValue, "Int16.MaxValue");
                yield return new Constant<Int64>(UInt16.MaxValue, "UInt16.MaxValue");
                yield return new Constant<Int64>(Int32.MaxValue, "Int32.MaxValue");
                yield return new Constant<Int64>(UInt32.MaxValue, "UInt32.MaxValue");
                yield return new Constant<Int64>(Int64.MaxValue, "Int64.MaxValue");
            }
        }
    }
}
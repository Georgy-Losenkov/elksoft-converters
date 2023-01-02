namespace Elksoft.Converters.Generator
{
    internal sealed class Int32TypeDescriptor : TypeDescriptor<Int32>
    {
        public override Boolean IsSigned => true;
        public override Byte Precision => 32;
        public override String ZeroLiteral => "0";
        public override String OneLiteral => "1";
        public override IEnumerable<Constant<Int32>> Constants
        {
            get
            {
                yield return new Constant<Int32>(Int32.MinValue, "Int32.MinValue");
                yield return new Constant<Int32>(Int16.MinValue, "Int16.MinValue");
                yield return new Constant<Int32>(SByte.MinValue, "SByte.MinValue");
                yield return new Constant<Int32>(-1, "-1");
                yield return new Constant<Int32>(0, "0");
                yield return new Constant<Int32>(1, "1");
                yield return new Constant<Int32>(SByte.MaxValue, "SByte.MaxValue");
                yield return new Constant<Int32>(Byte.MaxValue, "Byte.MaxValue");
                yield return new Constant<Int32>(Int16.MaxValue, "Int16.MaxValue");
                yield return new Constant<Int32>(UInt16.MaxValue, "UInt16.MaxValue");
                yield return new Constant<Int32>(Int32.MaxValue, "Int32.MaxValue");
            }
        }
    }
}
namespace Elksoft.Converters.Generator
{
    internal sealed class UInt32TypeDescriptor : TypeDescriptor<UInt32>, ITypeDescriptor
    {
        public override Boolean IsSigned => false;
        public override Byte Precision => 32;
        public override String ZeroLiteral => "0U";
        public override String OneLiteral => "1U";
        public override IEnumerable<Constant<UInt32>> Constants
        {
            get
            {
                yield return new Constant<UInt32>(0U, "0U");
                yield return new Constant<UInt32>(1U, "1U");
                yield return new Constant<UInt32>((UInt32)SByte.MaxValue, "(UInt32)SByte.MaxValue");
                yield return new Constant<UInt32>(Byte.MaxValue, "(UInt32)Byte.MaxValue");
                yield return new Constant<UInt32>((UInt32)Int16.MaxValue, "(UInt32)Int16.MaxValue");
                yield return new Constant<UInt32>(UInt16.MaxValue, "(UInt32)UInt16.MaxValue");
                yield return new Constant<UInt32>(Int32.MaxValue, "(UInt32)Int32.MaxValue");
                yield return new Constant<UInt32>(UInt32.MaxValue, "UInt32.MaxValue");
            }
        }
    }
}
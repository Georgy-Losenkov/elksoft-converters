namespace Elksoft.Converters.Generator
{
    internal sealed class ByteTypeDescriptor : TypeDescriptor<Byte>, ITypeDescriptor
    {
        public override Boolean IsSigned => false;
        public override Byte Precision => 8;
        public override String ZeroLiteral => "(Byte)0";
        public override String OneLiteral => "(Byte)1";
        public override IEnumerable<Constant<Byte>> Constants
        {
            get
            {
                yield return new Constant<Byte>(0, "0");
                yield return new Constant<Byte>(1, "1");
                yield return new Constant<Byte>(127, "127");
                yield return new Constant<Byte>(Byte.MaxValue, "Byte.MaxValue");
            }
        }
    }
}
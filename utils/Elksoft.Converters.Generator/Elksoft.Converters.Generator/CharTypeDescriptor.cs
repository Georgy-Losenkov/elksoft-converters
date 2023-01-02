namespace Elksoft.Converters.Generator
{
    internal sealed class CharTypeDescriptor : TypeDescriptor<Char>, ITypeDescriptor
    {
        public override Boolean IsSigned => false;
        public override Byte Precision => 16;
        public override String ZeroLiteral => "'\\u0000'";
        public override String OneLiteral => "'\\u0001'";
        public override IEnumerable<Constant<Char>> Constants
        {
            get
            {
                yield return new Constant<Char>('\u0000', "'\\u0000'");
                yield return new Constant<Char>('\u0001', "'\\u0001'");
                yield return new Constant<Char>((Char)SByte.MaxValue, "(Char)SByte.MaxValue");
                yield return new Constant<Char>((Char)Byte.MaxValue, "(Char)Byte.MaxValue");
                yield return new Constant<Char>((Char)Int16.MaxValue, "(Char)Int16.MaxValue");
                yield return new Constant<Char>(Char.MaxValue, "Char.MaxValue");
            }
        }
    }
}
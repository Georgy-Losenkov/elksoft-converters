namespace Elksoft.Converters.Generator
{
    internal sealed class DecimalTypeDescriptor : TypeDescriptor<Decimal>
    {
        public override Boolean IsSigned => true;
        public override Byte Precision => 96;
        public override String ZeroLiteral => "Decimal.Zero";
        public override String OneLiteral => "Decimal.One";
        public override IEnumerable<Constant<Decimal>> Constants
        {
            get
            {
                yield return new Constant<Decimal>(Decimal.MinValue, "Decimal.MinValue");
                yield return new Constant<Decimal>(Int64.MinValue, "Int64.MinValue");
                yield return new Constant<Decimal>(Int32.MinValue, "Int32.MinValue");
                yield return new Constant<Decimal>(Int16.MinValue, "Int16.MinValue");
                yield return new Constant<Decimal>(SByte.MinValue, "SByte.MinValue");
                yield return new Constant<Decimal>(-1m, "-1m");
                yield return new Constant<Decimal>(Decimal.Zero, "Decimal.Zero");
                yield return new Constant<Decimal>(Decimal.One, "Decimal.One");
                yield return new Constant<Decimal>(SByte.MaxValue, "SByte.MaxValue");
                yield return new Constant<Decimal>(Byte.MaxValue, "Byte.MaxValue");
                yield return new Constant<Decimal>(Int16.MaxValue, "Int16.MaxValue");
                yield return new Constant<Decimal>(UInt16.MaxValue, "UInt16.MaxValue");
                yield return new Constant<Decimal>(Int32.MaxValue, "Int32.MaxValue");
                yield return new Constant<Decimal>(UInt32.MaxValue, "UInt32.MaxValue");
                yield return new Constant<Decimal>(Int64.MaxValue, "Int64.MaxValue");
                yield return new Constant<Decimal>(UInt64.MaxValue, "UInt64.MaxValue");
                yield return new Constant<Decimal>(Decimal.MaxValue, "Decimal.MaxValue");
            }
        }
    }
}
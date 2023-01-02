namespace Elksoft.Converters.Generator
{
    internal interface IConstant
    {
        Object Value { get; }

        String Literal { get; }

        NetVersion MinVersion { get; }

        Boolean X64Only { get; }
    }

    internal class Constant<T> : IConstant
    {
        public Constant(T value, String literal)
        {
            Value = value;
            Literal = literal;
            MinVersion = NetVersion.NetStandard;
            X64Only = false;
        }

        public T Value { get; }
        public String Literal { get; }
        public NetVersion MinVersion { get; init; }
        public Boolean X64Only { get; init; }
        Object IConstant.Value { get { return Value!; } }

        public override String ToString()
        {
            return $"(Value: {Value}, Literal: {Literal})";
        }
    }
}
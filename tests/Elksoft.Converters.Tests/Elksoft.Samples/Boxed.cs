namespace Elksoft.Samples
{
    public sealed class Boxed<T> : IBoxed<T>
        where T : struct
    {
        public Boxed(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}
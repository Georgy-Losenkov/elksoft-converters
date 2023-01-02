namespace Elksoft.Samples
{
    public interface IBoxed<T>
        where T : struct
    {
        T Value { get; }
    }
}
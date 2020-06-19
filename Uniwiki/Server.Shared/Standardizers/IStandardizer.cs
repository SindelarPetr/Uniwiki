namespace Shared.Standardizers
{
    public interface IStandardizer<T>
    {
        T Standardize(T model);
    }
}

namespace TheWitcher.Domain
{
    public interface IMapper<T, DTO>
    {
        DTO AutoMap(T item);
    }
}

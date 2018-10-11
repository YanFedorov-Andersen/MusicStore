namespace MusicStore.Domain
{
    public interface IMapper<T, DTO>
    {
        DTO AutoMap(T item);
        T ReAutoMap(DTO item, T initialItem);
    }
}

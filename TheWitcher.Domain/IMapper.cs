using MusicStore.DataAccess;

namespace MusicStore.Domain
{
    public interface IMapper<T, DTO> 
        where T: Entity
        where DTO: Entity
    {
        DTO AutoMap(T item);
        T ReAutoMap(DTO item, T initialItem);
    }
}

using MusicStore.DataAccess;
namespace MusicStore.Domain.Mappers
{
    public class MapUserAccount : IMapper<User, UserAccountDTO>
    {
        public UserAccountDTO AutoMap(User item)
        {
            UserAccountDTO userAccountDTO = new UserAccountDTO()
            {
                Id = item.Id,
                BoughtSongs = item.BoughtSongs,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Money = item.Money
            };
            return userAccountDTO;
        }
    }
}

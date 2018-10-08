using MusicStore.DataAccess;
namespace MusicStore.Domain.Mappers
{
    public class MapUserAccount : IMapper<User, UserAccount>
    {
        public UserAccount AutoMap(User item)
        {
            UserAccount userAccountDTO = new UserAccount()
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

using MusicStore.DataAccess;
using MusicStore.Domain.DataTransfer;

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
                Money = item.Money,
                IsActive = item.IsActive
            };
            return userAccountDTO;
        }

        public User ReAutoMap(UserAccount userDomain, User userDataAccess)
        {
            userDataAccess.FirstName = userDomain.FirstName;
            userDataAccess.LastName = userDomain.LastName;
            userDataAccess.Money = userDomain.Money;
            return userDataAccess;
        }
    }
}

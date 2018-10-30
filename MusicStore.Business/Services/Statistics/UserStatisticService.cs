using MusicStore.Business.Interfaces;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Business.Services.Statistics
{
    public class UserStatisticService: IUserStatisticService
    {
        private readonly IRepository<User> _userRepository;

        public UserStatisticService(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.UserAccountRepository;
        }
        public int GetTotalNumberOfSongs(int userId)
        {
            if(userId < 1)
            {
                throw new ArgumentException("userId is less then 1", nameof(userId));
            }

            var boughtSongs = GetBoughtSongs(userId);

            if (boughtSongs == null)
            {
                return 0;
            }

            return boughtSongs.Count();
        }

        public decimal GetTotalSpentMoney(int userId)
        {
            if (userId < 1)
            {
                throw new ArgumentException("userId is less then 1", nameof(userId));
            }

            var boughtSongs = GetBoughtSongs(userId);

            if (boughtSongs == null)
            {
                return 0;
            }

            decimal totalSpentMoney = boughtSongs.Select(x => x.BoughtPrice).Sum();

            return totalSpentMoney;
        }
        private IEnumerable<BoughtSong> GetBoughtSongs(int userId)
        {
            if (userId < 1)
            {
                throw new ArgumentException("userId is less then 1", nameof(userId));
            }

            var user = _userRepository.GetItem(userId);

            if (user == null)
            {
                throw new Exception("Something wrong with user in dataBase");
            }

            if (user.BoughtSongs == null)
            {
                return null;
            }

            return user.BoughtSongs;
        }
    }
}

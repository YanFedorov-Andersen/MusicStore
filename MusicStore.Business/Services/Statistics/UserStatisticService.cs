﻿using MusicStore.Business.Interfaces;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using System;
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

            var user = _userRepository.GetItem(userId);
            
            if(user == null || user.BoughtSongs == null)
            {
                throw new Exception("Something wrong with user in dataBase");
            }

            return user.BoughtSongs.Count();
        }

        public decimal GetTotalSpentMoney(int userId)
        {
            if (userId < 1)
            {
                throw new ArgumentException("userId is less then 1", nameof(userId));
            }

            var user = _userRepository.GetItem(userId);

            if (user == null || user.BoughtSongs == null)
            {
                throw new Exception("Something wrong with user in dataBase");
            }

            decimal totalSpentMoney = 0;

            foreach(var song in user.BoughtSongs)
            {
                totalSpentMoney += song.BoughtPrice;
            }

            return totalSpentMoney;
        }
    }
}

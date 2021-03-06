﻿using System;
using MusicStore.Business.Interfaces;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using MusicStore.Domain;
using MusicStore.Domain.DataTransfer;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Business.Services
{
    public class AdminService: IAdminService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper<User,UserAccount> _mapUser;
        public AdminService(IUnitOfWork unitOfWork, IMapper<User, UserAccount> mapUser)
        {
            _userRepository = unitOfWork.UserAccountRepository;
            _adminRepository = unitOfWork.AdminRepository;
            _mapUser = mapUser;
        }

        public IList<UserAccount> GetActiveOrNotActiveUsers(bool isActive)
        {
            var usersAccountList = new List<UserAccount>();
            var usersAccounts = _adminRepository.GetActiveOrNotActiveUsers(isActive);
            if (usersAccounts == null)
            {
                throw new Exception("Such users not exists");
            }
            usersAccountList = usersAccounts.Select(_mapUser.AutoMap).ToList();
            return usersAccountList;
        }

        public IList<UserAccount> GetFullListOfUsers()
        {
            var usersAccounts = _userRepository.GetItemList();

            if (usersAccounts == null || !usersAccounts.Any())
            {
                return null;
            }

            return usersAccounts.Select(_mapUser.AutoMap).ToList();
        }
    }
}

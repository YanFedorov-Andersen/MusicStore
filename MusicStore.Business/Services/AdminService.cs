using System;
using MusicStore.Business.Interfaces;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using MusicStore.Domain;
using MusicStore.Domain.DataTransfer;
using System.Collections.Generic;
using System.Linq;
using MusicStore.Domain.Mappers;

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

        public bool EditUserAccount(UserAccount userDomain)
        {
            if (userDomain == null)
            {
                throw new ArgumentNullException("userDomain", "Can not update user, because it is null");
            }

            if (userDomain.Id < 1)
            {
                throw new ArgumentException("user Id is less then 1", $"{nameof(userDomain.Id)}");
            }

            var userDataAccess = _userRepository.GetItem(userDomain.Id);

            var updatedUser = ReverseAutoMap(userDomain, userDataAccess);

            int result = _userRepository.Update(updatedUser);

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public User ReverseAutoMap(UserAccount userDomain, User userDataAccess)
        {
            userDataAccess.FirstName = userDomain.FirstName;
            userDataAccess.LastName = userDomain.LastName;
            userDataAccess.IsActive = userDomain.IsActive;
            return userDataAccess;
        }
    }
}

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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper<User,UserAccount> _mapUser;
        public AdminService(IUnitOfWork unitOfWork, IMapper<User, UserAccount> mapUser)
        {
            _userRepository = unitOfWork.UserAccount;
            _userAccountRepository = unitOfWork.UserAccountRepository;
            _adminRepository = unitOfWork.AdminRepository;
            _mapUser = mapUser;
        }

        //public int ManageUserAccount(UserAccount domainUser)
        //{
        //    if (domainUser == null)
        //    {
        //        throw new ArgumentException("domainUser has null value in AdminService ManageUserAccount");
        //    }

        //    User user = _userRepository.GetItem(domainUser.Id);
        //    _mapUser.ReAutoMap(domainUser, user);
        //    var result = _userRepository.Update(user);
        //    return result;
        //}

        public List<UserAccount> GetListOfUsers(bool isActive)
        {
            List<UserAccount> usersAccountList = new List<UserAccount>();

            if (isActive)
            {
                usersAccountList =_adminRepository.ActiveUsers().Select(_mapUser.AutoMap).ToList();                
            }
            else
            {
                usersAccountList = _adminRepository.NotActiveUsers().Select(_mapUser.AutoMap).ToList();
            }

            return usersAccountList;
        }

        public List<UserAccount> GetFullListOfUsers()
        {
            List<UserAccount> usersAccountList = new List<UserAccount>();
            usersAccountList = _userRepository.GetItemList().Select(_mapUser.AutoMap).ToList();
            return usersAccountList;
        }
    }
}

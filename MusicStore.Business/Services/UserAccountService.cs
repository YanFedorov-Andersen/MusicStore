using MusicStore.Business.Interfaces;
using MusicStore.DataAccess.Interfaces;
using System;
using MusicStore.DataAccess;
using MusicStore.Domain;
using MusicStore.Domain.DataTransfer;

namespace MusicStore.Business.Services
{
    public class UserAccountService: IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper<User, Domain.DataTransfer.UserAccount> _mapUser;

        public UserAccountService(IUnitOfWork unitOfWork, IMapper<User, Domain.DataTransfer.UserAccount> mapUser)
        {
            _userAccountRepository = unitOfWork.UserAccountRepository;
            _userRepository = unitOfWork.UserAccount;
            _mapUser = mapUser;
        }

        public bool RegisterUserAccount(string identityId)
        {
            var result = _userAccountRepository.CreateWithGuidId(identityId);
            if (result)
            {
                return true;
            }
            throw new Exception("Internal server error: can not create user");
        }

        public bool EditUserAccount(UserAccount userDomain)
        {
            if (userDomain != null)
            {
                var userDataAccess = _userRepository.GetItem(userDomain.Id);

                var updatedUser = _mapUser.ReAutoMap(userDomain, userDataAccess);

                int result = _userRepository.Update(updatedUser);

                if (result > 0)
                {
                    return true;
                }
            }
            throw new ArgumentException("Can not update user, because it is null");
        }

        public Domain.DataTransfer.UserAccount GetUserData(string identityId)
        {
            if (identityId == null)
            {
                throw new ArgumentException("User id is not valid");
            }

            var result = _userAccountRepository.GetItemWithGuidId(identityId);

            if (result == null)
            {
                throw new Exception("User not found");
            }

            return _mapUser.AutoMap(result);
        }

        public int ConvertGuidInStringIdToIntId(string identityId)
        {
               return _userAccountRepository.GetUserId(identityId);
        }
    }
}

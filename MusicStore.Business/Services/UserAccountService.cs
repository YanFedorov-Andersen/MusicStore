using MusicStore.Business.Interfaces;
using MusicStore.DataAccess.Interfaces;
using System;
using System.Linq;
using MusicStore.DataAccess;
using MusicStore.Domain;
using MusicStore.Domain.DataTransfer;

namespace MusicStore.Business.Services
{
    public class UserAccountService: IUserAccountService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper<User, Domain.DataTransfer.UserAccount> _mapUser;

        public UserAccountService(IUnitOfWork unitOfWork, IMapper<User, Domain.DataTransfer.UserAccount> mapUser)
        {
            _userRepository = unitOfWork.UserAccount;
            _mapUser = mapUser;
        }

        public bool RegisterUserAccount(string identityId)
        {
            if (string.IsNullOrEmpty(identityId))
            {
                throw new ArgumentException("User id is not valid");
            }

            var resultOfParse = Guid.TryParse(identityId, out var guidIdentityId);

            if (resultOfParse == false)
            {
                throw new ArgumentException("Can not parse string to guid");
            }

            var result = CreateWithGuidId(guidIdentityId);
            if (result)
            {
                return true;
            }
            throw new Exception("Internal server error: can not create user");
        }

        public bool EditUserAccount(UserAccount userDomain)
        {
            if (userDomain == null)
            {
                throw new ArgumentException("Can not update user, because it is null");
            }

            var userDataAccess = _userRepository.GetItem(userDomain.Id);

            var updatedUser = _mapUser.ReAutoMap(userDomain, userDataAccess);

            int result = _userRepository.Update(updatedUser);

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public Domain.DataTransfer.UserAccount GetUserData(string identityId)
        {
            if (string.IsNullOrEmpty(identityId))
            {
                throw new ArgumentException("User id is not valid");
            }

            var resultOfParse = Guid.TryParse(identityId, out var guidIdentityId);

            if (resultOfParse == false)
            {
                throw new ArgumentException("Can not parse string to guid");
            }

            var result = GetItemWithGuidId(guidIdentityId);

            if (result == null)
            {
                throw new Exception("User not found");
            }

            return _mapUser.AutoMap(result);
        }

        public int ConvertGuidInStringIdToIntId(string identityId)
        {
            if (string.IsNullOrEmpty(identityId))
            {
                throw new ArgumentException("User id is not valid");
            }

            var resultOfParse = Guid.TryParse(identityId, out var guidIdentityId);

            if (resultOfParse == false)
            {
                throw new ArgumentException("Can not parse string to guid");
            }

            return GetUserId(guidIdentityId);
        }
        private bool CreateWithGuidId(Guid identityId)
        {
            if (identityId == null)
            {
                throw new ArgumentException("Invalid user identity id");
            }

            User user = new User(identityId);
            var result = _userRepository.Create(user);

            if(result < 0)
            {
                return false;
            }

            return true;
        }

        private int GetUserId(Guid identityId)
        {
            if (identityId == null)
            {
                throw new ArgumentException("Invalid user identity id");
            }

            var user = _userRepository.GetItemList().FirstOrDefault(x => x.IdentityKey == identityId);

            if (user != null)
            {
                return user.Id;
            }

            throw new Exception("User can not found or found more then one");
        }
        private User GetItemWithGuidId(Guid id)
        {
            var usersList = _userRepository.GetItemList();
            return usersList.FirstOrDefault(x => x.IdentityKey == id);
        }
    }
}

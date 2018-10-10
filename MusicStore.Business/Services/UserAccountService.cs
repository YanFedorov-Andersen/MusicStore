using MusicStore.Business.Interfaces;
using MusicStore.DataAccess.Interfaces;
using System;

namespace MusicStore.Business.Services
{
    public class UserAccountService: IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IUnitOfWork unitOfWork)
        {
            _userAccountRepository = unitOfWork.UserAccountRepository;
        }

        public bool RegisterUserAccount(string Id)
        {
            var result = _userAccountRepository.CreateWithGuidId(Id);
            if (result)
            {
                return true;
            }
            throw new Exception("Internal server error: can not create user");
        }
    }
}

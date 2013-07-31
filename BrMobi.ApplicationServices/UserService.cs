using BrMobi.ApplicationServices.ServiceInterfaces;
using BrMobi.Core;
using BrMobi.Core.Entities;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Service.ApplicationServices;

namespace BrMobi.ApplicationServices
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User GetByFacebookId(string facebookId)
        {
            return userRepository.GetByFacebookId(facebookId);
        }

        public void Create(User user)
        {
            userRepository.Create(user);
        }
    }
}
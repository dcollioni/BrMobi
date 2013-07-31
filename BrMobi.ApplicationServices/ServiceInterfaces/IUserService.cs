using BrMobi.Core;
using BrMobi.Core.Entities;

namespace BrMobi.ApplicationServices.ServiceInterfaces
{
    public interface IUserService
    {
        User GetByFacebookId(string facebookId);
        void Create(User user);
    }
}
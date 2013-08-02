using BrMobi.Core.Entities;

namespace BrMobi.Core.RepositoryInterfaces
{
    public interface IPreUserRepository
    {
        PreUser Create(PreUser entity);
        int Count(string email);
    }
}
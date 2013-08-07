using BrMobi.Core.Entities;
using System.Collections.Generic;

namespace BrMobi.Core.RepositoryInterfaces
{
    public interface IPreUserRepository
    {
        PreUser Create(PreUser entity);
        int Count(string email);
        IList<PreUser> GetAll();
    }
}
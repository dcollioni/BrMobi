using System.Collections.Generic;
using BrMobi.Core.Entities;

namespace BrMobi.Core.RepositoryInterfaces
{
    public interface ICityRepository
    {
        IList<City> ListAll(int stateId);
        City Get(int id);
    }
}
using System.Collections.Generic;

namespace BrMobi.Core.RepositoryInterfaces
{
    public interface ICityRepository
    {
        IList<City> ListAll(int stateId);
        City Get(int id);
    }
}
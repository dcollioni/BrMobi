using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Entities;
using BrMobi.Core.RepositoryInterfaces;

namespace BrMobi.Data.Repositories
{
    public class CityRepository : BaseRepository, ICityRepository
    {
        public IList<City> ListAll(int stateId)
        {
            return Session.Query<City>(c => c.State.Id == stateId).OrderBy(s => s.Name).ToList();
        }

        public City Get(int id)
        {
            return Session.Query<City>(c => c.Id == id).SingleOrDefault();
        }
    }
}
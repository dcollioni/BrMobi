using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o
{
    public class CityRepository : BaseRepository, ICityRepository
    {
        public CityRepository(Db4objects.Db4o.IObjectServer server)
            : base(server)
        {
        }

        public IList<City> ListAll(int stateId)
        {
            var cities = new List<City>();

            using (var client = Server.OpenClient())
            {
                cities = client.Query<City>(c => c.State.Id == stateId).OrderBy(s => s.Name).ToList();
            }

            return cities;
        }

        public City Get(int id)
        {
            City city = null;

            using (var client = Server.OpenClient())
            {
                city = client.Query<City>(c => c.Id == id).SingleOrDefault();
            }

            return city;
        }
    }
}
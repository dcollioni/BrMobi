using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o
{
    public class CityRepository : BaseRepository, ICityRepository
    {
        public IList<City> ListAll(int stateId)
        {
            var cities = new List<City>();

            using (var client = Client)
            {
                cities = client.Query<City>(c => c.State.Id == stateId).OrderBy(s => s.Name).ToList();
            }

            return cities;
        }

        public City Get(int id)
        {
            City city = null;

            //using (var server = Server)
            //{
                using (var client = Client)
                {
                    city = client.Query<City>(c => c.Id == id).SingleOrDefault();
                }
            //}

            return city;
        }
    }
}
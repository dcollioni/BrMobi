using System.Collections.Generic;
using BrMobi.ApplicationServices.ServiceInterfaces;
using BrMobi.Core;
using BrMobi.Core.Entities;
using BrMobi.Core.RepositoryInterfaces;

namespace BrMobi.ApplicationServices
{
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public IList<City> ListAll(int stateId)
        {
            return cityRepository.ListAll(stateId);
        }

        public City Get(int id)
        {
            return cityRepository.Get(id);
        }
    }
}
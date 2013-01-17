using System.Collections.Generic;
using BrMobi.Core;
using BrMobi.Core.Entities;

namespace BrMobi.ApplicationServices.ServiceInterfaces
{
    public interface ICityService
    {
        IList<City> ListAll(int stateId);
        City Get(int id);
    }
}

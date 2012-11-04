using System.Collections.Generic;
using BrMobi.Core;

namespace BrMobi.ApplicationServices.ServiceInterfaces
{
    public interface ICityService
    {
        IList<City> ListAll(int stateId);
        City Get(int id);
    }
}

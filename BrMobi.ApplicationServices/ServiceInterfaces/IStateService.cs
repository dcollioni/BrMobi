using System.Collections.Generic;
using BrMobi.Core;
using BrMobi.Core.Entities;

namespace BrMobi.ApplicationServices.ServiceInterfaces
{
    public interface IStateService
    {
        IList<State> ListAll();
    }
}

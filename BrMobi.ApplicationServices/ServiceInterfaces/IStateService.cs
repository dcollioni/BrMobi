using System.Collections.Generic;
using BrMobi.Core;

namespace BrMobi.ApplicationServices.ServiceInterfaces
{
    public interface IStateService
    {
        IList<State> ListAll();
    }
}

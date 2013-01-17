using System.Collections.Generic;
using BrMobi.Core.Entities;

namespace BrMobi.Core.RepositoryInterfaces
{
    public interface IStateRepository
    {
        IList<State> ListAll();
    }
}
using System.Collections.Generic;

namespace BrMobi.Core.RepositoryInterfaces
{
    public interface IStateRepository
    {
        IList<State> ListAll();
    }
}
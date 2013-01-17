using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Entities;
using BrMobi.Core.RepositoryInterfaces;

namespace BrMobi.Data.Repositories
{
    public class StateRepository : BaseRepository, IStateRepository
    {
        public IList<State> ListAll()
        {
            return Session.Query<State>().OrderBy(s => s.Code).ToList();
        }
    }
}
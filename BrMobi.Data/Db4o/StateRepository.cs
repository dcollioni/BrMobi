using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o
{
    public class StateRepository : BaseRepository, IStateRepository
    {
        public IList<State> ListAll()
        {
            var states = new List<State>();

            using (var client = Client)
            {
                states = client.Query<State>().OrderBy(s => s.Code).ToList();
            }

            return states;
        }
    }
}
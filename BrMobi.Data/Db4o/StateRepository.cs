using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o
{
    public class StateRepository : BaseRepository, IStateRepository
    {
        public StateRepository(Db4objects.Db4o.IObjectServer server)
            : base(server)
        {
        }

        public IList<State> ListAll()
        {
            var states = new List<State>();

            using (var client = Server.OpenClient())
            {
                states = client.Query<State>().OrderBy(s => s.Code).ToList();
            }

            return states;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o
{
    public class StateRepository : BaseRepository, IStateRepository
    {
        private readonly Db4objects.Db4o.IObjectServer server;

        public StateRepository(Db4objects.Db4o.IObjectServer server)
        {
            this.server = server;
        }

        public IList<State> ListAll()
        {
            var states = new List<State>();

            using (var client = server.OpenClient())
            {
                states = client.Query<State>().OrderBy(s => s.Code).ToList();
            }

            return states;
        }
    }
}
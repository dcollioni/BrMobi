using System;
using System.Linq;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces.Map;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o.Map
{
    public class BusLineRepository : BaseRepository, IBusLineRepository
    {
        private readonly Db4objects.Db4o.IObjectServer server;

        public BusLineRepository(Db4objects.Db4o.IObjectServer server)
        {
            this.server = server;
        }

        public BusLine Create(BusLine busLine)
        {
            //using (var server = Server)
            //{
                using (var client = server.OpenClient())
                {
                    busLine.Id = busLine.GetHashCode();
                    client.Store(busLine);
                }
            //}

            return busLine;
        }

        public BusLine Get(string name)
        {
            BusLine busLine = null;

            //using (var server = Server)
            //{
                using (var client = server.OpenClient())
                {
                    busLine = client.Query<BusLine>(b => b.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                                    .SingleOrDefault();
                }
            //}

            return busLine;
        }
    }
}
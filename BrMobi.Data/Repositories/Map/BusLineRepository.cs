using System.Linq;
using BrMobi.Core.Entities.Map;
using BrMobi.Core.Extensions;
using BrMobi.Core.RepositoryInterfaces.Map;

namespace BrMobi.Data.Repositories.Map
{
    public class BusLineRepository : BaseRepository, IBusLineRepository
    {
        public BusLine Create(BusLine busLine)
        {
            busLine.Id = busLine.GetHashCode();
            Session.Store(busLine);
            return busLine;
        }

        public BusLine Get(string name)
        {
            return Session.Query<BusLine>(b => b.Name.InvariantEquals(name)).SingleOrDefault();
        }
    }
}
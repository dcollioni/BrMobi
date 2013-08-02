using System;
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Entities.Map;
using BrMobi.Core.RepositoryInterfaces;

namespace BrMobi.Data.Repositories.Map
{
    public class MarkerRepository : BaseRepository, IMarkerRepository
    {
        public IList<Marker> List(int ownerId, int page)
        {
            const int pageSize = 5;
            var from = ((page - 1) * pageSize) + 1;

            var results = Session.Query<Marker>(m => m.Owner.Id == ownerId)
                          .OrderByDescending(x => x.CreatedOn)
                          .Skip(from - 1)
                          .Take(pageSize)
                          .ToList();
            return results;
        }

        public void DeleteAll(int ownerId)
        {
            foreach (var ride in Session.Query<Marker>(m => m.Owner.Id == ownerId))
            {
                Session.Delete(ride);
            }
        }
    }
}
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces.Map;
using BrMobi.Data.Db4o.Base;
using System.Collections.Generic;

namespace BrMobi.Data.Db4o.Map
{
    public class HelpMarkerRepository : BaseRepository, IHelpMarkerRepository
    {
        public void Create(HelpMarker helpMarker)
        {
            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    helpMarker.Owner = client.Query<User>(u => u.Email == helpMarker.Owner.Email).SingleOrDefault();
                    helpMarker.Id = helpMarker.GetHashCode();
                    client.Store(helpMarker);
                }
            }
        }

        public IList<HelpMarker> List(LatLng southWest, LatLng northEast)
        {
            var markers = new List<HelpMarker>();

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    markers = client.Query<HelpMarker>(m => southWest.Lat() <= m.Lat && m.Lat <= northEast.Lat()
                                                        && southWest.Lng() <= m.Lng && m.Lng <= northEast.Lng()).ToList();
                }
            }

            return markers;
        }
    }
}
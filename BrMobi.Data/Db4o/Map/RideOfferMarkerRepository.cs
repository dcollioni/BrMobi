using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces.Map;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o.Map
{
    public class RideOfferMarkerRepository : BaseRepository, IRideOfferMarkerRepository
    {
        public void Create(RideOfferMarker rideOfferMarker)
        {
            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    rideOfferMarker.Owner = client.Query<User>(u => u.Email == rideOfferMarker.Owner.Email).SingleOrDefault();
                    rideOfferMarker.Id = rideOfferMarker.GetHashCode();
                    client.Store(rideOfferMarker);
                }
            }
        }

        public IList<RideOfferMarker> List(LatLng southWest, LatLng northEast)
        {
            var markers = new List<RideOfferMarker>();

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    markers = client.Query<RideOfferMarker>(m => southWest.Lat() <= m.Lat && m.Lat <= northEast.Lat()
                                                        && southWest.Lng() <= m.Lng && m.Lng <= northEast.Lng()).ToList();
                }
            }

            return markers;
        }
    }
}
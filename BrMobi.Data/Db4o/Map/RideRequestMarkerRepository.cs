using System.Linq;
using BrMobi.Core;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces.Map;
using BrMobi.Data.Db4o.Base;
using System.Collections.Generic;

namespace BrMobi.Data.Db4o.Map
{
    public class RideRequestMarkerRepository : BaseRepository, IRideRequestMarkerRepository
    {
        public void Create(RideRequestMarker rideRequestMarker)
        {
            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    rideRequestMarker.Owner = client.Query<User>(u => u.Email == rideRequestMarker.Owner.Email).SingleOrDefault();
                    rideRequestMarker.Id = rideRequestMarker.GetHashCode();
                    client.Store(rideRequestMarker);
                }
            }
        }

        public IList<RideRequestMarker> List(LatLng southWest, LatLng northEast)
        {
            var markers = new List<RideRequestMarker>();

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    markers = client.Query<RideRequestMarker>(m => southWest.Lat() <= m.Lat && m.Lat <= northEast.Lat()
                                                        && southWest.Lng() <= m.Lng && m.Lng <= northEast.Lng()).ToList();
                }
            }

            return markers;
        }
    }
}
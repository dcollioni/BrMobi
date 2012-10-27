using System.Collections.Generic;
using BrMobi.Core.Map;

namespace BrMobi.Core.RepositoryInterfaces.Map
{
    public interface IRideRequestMarkerRepository
    {
        void Create(RideRequestMarker rideRequestMarker);
        IList<RideRequestMarker> List(LatLng southWest, LatLng northEast);
    }
}
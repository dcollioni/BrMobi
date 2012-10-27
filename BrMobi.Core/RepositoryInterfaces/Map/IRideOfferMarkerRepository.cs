using System.Collections.Generic;
using BrMobi.Core.Map;

namespace BrMobi.Core.RepositoryInterfaces.Map
{
    public interface IRideOfferMarkerRepository
    {
        void Create(RideOfferMarker rideOfferMarker);
        IList<RideOfferMarker> List(LatLng southWest, LatLng northEast);
    }
}
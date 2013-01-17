using System;
using System.Collections.Generic;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Map;

namespace BrMobi.Core.RepositoryInterfaces.Map
{
    public interface IRideRequestMarkerRepository
    {
        void Create(RideRequestMarker rideRequestMarker);
        IList<RideRequestMarker> List(LatLng southWest, LatLng northEast, User loggedUser);
        RideRequestMarker Get(int id);
        RideRequestMarker Update(DateTime dateTime, string destination, int id);
        RideRequestMarker AddOffer(int rideRequestId, int offerId);
        RideRequestMarker RemoveOffer(int rideRequestId, int offerId);
        void Remove(int markerId);
    }
}
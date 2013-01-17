using System;
using System.Collections.Generic;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Map;

namespace BrMobi.Core.RepositoryInterfaces.Map
{
    public interface IRideOfferMarkerRepository
    {
        void Create(RideOfferMarker rideOfferMarker);
        IList<RideOfferMarker> List(LatLng southWest, LatLng northEast, User loggedUser);
        RideOfferMarker Get(int id);
        RideOfferMarker Update(DateTime dateTime, string destination, int id);
        RideOfferMarker AddHitchhiker(int rideOfferId, int hitchhikerId);
        RideOfferMarker RemoveHitchhiker(int rideOfferId, int hitchhikerId);
        void Remove(int markerId);
    }
}
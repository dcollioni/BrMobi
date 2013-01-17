using System.Collections.Generic;
using BrMobi.Core;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Map;
using BrMobi.Core.ViewModels.Map;

namespace BrMobi.ApplicationServices.ServiceInterfaces.Map
{
    public interface IMapService
    {
        BusMarker MarkBus(BusMarker busMarker);
        RideOfferMarker MarkRideOffer(RideOfferMarker rideOfferMarker);
        RideRequestMarker MarkRideRequest(RideRequestMarker rideRequestMarker);
        HelpMarker MarkHelp(HelpMarker helpMarker);
        IList<MarkerViewModel> ListMarkers(LatLng southWest, LatLng northEast, User loggedUser);
    }
}
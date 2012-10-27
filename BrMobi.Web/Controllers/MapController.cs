using System.Web.Mvc;
using BrMobi.ApplicationServices.ServiceInterfaces.Map;
using BrMobi.Core.Map;
using BrMobi.Web.Attributes;

namespace BrMobi.Web.Controllers
{
    [BrMobiAuthorize]
    public class MapController : BaseController
    {
        private readonly IMapService mapService;
        private readonly IBusLineService busLineService;
        private readonly IBusMarkerService busMarkerService;

        public MapController(IMapService mapService,
                             IBusLineService busLineService,
                             IBusMarkerService busMarkerService)
        {
            this.mapService = mapService;
            this.busLineService = busLineService;
            this.busMarkerService = busMarkerService;
        }

        public JsonResult MarkBus(double lat, double lng)
        {
            var busMarker = new BusMarker(lat, lng, LoggedUser, GetBusMarkerImage());
            mapService.MarkBus(busMarker);
            return Json(busMarker);
        }

        public JsonResult MarkRideOffer(double lat, double lng)
        {
            var rideOfferMarker = new RideOfferMarker(lat, lng, LoggedUser, GetRideOfferMarkerImage());
            mapService.MarkRideOffer(rideOfferMarker);
            return Json(rideOfferMarker);
        }

        public JsonResult MarkRideRequest(double lat, double lng)
        {
            var rideRequestMarker = new RideRequestMarker(lat, lng, LoggedUser, GetRideRequestMarkerImage());
            mapService.MarkRideRequest(rideRequestMarker);
            return Json(rideRequestMarker);
        }

        public JsonResult MarkHelp(double lat, double lng)
        {
            var helpMarker = new HelpMarker(lat, lng, LoggedUser, GetHelpMarkerImage());
            mapService.MarkHelp(helpMarker);
            return Json(helpMarker);
        }

        public JsonResult LoadMarkers(double sLat, double sLng, double nLat, double nLng)
        {
            var southWest = new LatLng(sLat, sLng);
            var northEast = new LatLng(nLat, nLng);

            var markers = mapService.ListMarkers(southWest, northEast);
            return Json(markers);
        }

        public string GetBusInfo(int id)
        {
            var infoTemplate = GetBusInfoTemplate();
            var infoContent = busMarkerService.GetBusMarkerInfoDetails(id, infoTemplate);

            return infoContent;
        }

        public JsonResult CreateBusLine(string name, string url, int busMarkerId)
        {
            var busLine = new BusLine(name, url);
            busLine = busLineService.Create(busLine, busMarkerId);

            return Json(busLine);
        }
    }
}
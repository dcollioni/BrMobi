using System.Web.Mvc;
using System.Linq;
using BrMobi.ApplicationServices.ServiceInterfaces.Map;
using BrMobi.Core.Map;
using BrMobi.Web.Attributes;
using BrMobi.Core.Enums;

namespace BrMobi.Web.Controllers
{
    [BrMobiAuthorize]
    public class MapController : BaseController
    {
        private readonly IMapService mapService;
        private readonly IBusLineService busLineService;
        private readonly IBusMarkerService busMarkerService;
        private readonly IRideOfferMarkerService rideOfferMarkerService;

        public MapController(IMapService mapService,
                             IBusLineService busLineService,
                             IBusMarkerService busMarkerService,
                             IRideOfferMarkerService rideOfferMarkerService)
        {
            this.mapService = mapService;
            this.busLineService = busLineService;
            this.busMarkerService = busMarkerService;
            this.rideOfferMarkerService = rideOfferMarkerService;
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
            markers.ToList().ForEach(m => m.ImagePath = GetMarkerImage(m.Type));

            return Json(markers);
        }

        public string GetBusInfo(int id)
        {
            var infoTemplate = GetMarkerInfoTemplate(MarkerType.Bus);
            var infoContent = busMarkerService.GetBusMarkerInfoDetails(id, infoTemplate);

            return infoContent;
        }

        public JsonResult CreateBusLine(string name, string url, int busMarkerId)
        {
            var busLine = new BusLine(name, url);
            busLine = busLineService.Create(busLine, busMarkerId);

            return Json(busLine);
        }

        public string GetRideOfferInfo(int id)
        {
            var infoTemplate = GetMarkerInfoTemplate(MarkerType.RideOffer);
            var infoContent = rideOfferMarkerService.GetRideOfferMarkerInfoDetails(id, infoTemplate);

            return infoContent;
        }
    }
}
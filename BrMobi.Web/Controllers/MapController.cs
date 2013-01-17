using System;
using System.Linq;
using System.Web.Mvc;
using BrMobi.ApplicationServices.ServiceInterfaces.Map;
using BrMobi.Core.Entities.Map;
using BrMobi.Core.Enums;
using BrMobi.Web.Attributes;

namespace BrMobi.Web.Controllers
{
    [BrMobiAuthorize]
    public class MapController : BaseController
    {
        private readonly IMapService mapService;
        private readonly IBusLineService busLineService;
        private readonly IBusMarkerService busMarkerService;
        private readonly IRideOfferMarkerService rideOfferMarkerService;
        private readonly IRideRequestMarkerService rideRequestMarkerService;
        private readonly IHelpMarkerService helpMarkerService;

        public MapController(IMapService mapService,
                             IBusLineService busLineService,
                             IBusMarkerService busMarkerService,
                             IRideOfferMarkerService rideOfferMarkerService,
                             IRideRequestMarkerService rideRequestMarkerService,
                             IHelpMarkerService helpMarkerService)
        {
            this.mapService = mapService;
            this.busLineService = busLineService;
            this.busMarkerService = busMarkerService;
            this.rideOfferMarkerService = rideOfferMarkerService;
            this.rideRequestMarkerService = rideRequestMarkerService;
            this.helpMarkerService = helpMarkerService;
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

            var markers = mapService.ListMarkers(southWest, northEast, LoggedUser);
            markers.ToList().ForEach(m => m.ImagePath = GetMarkerImage(m.Type));

            return Json(markers);
        }

        public string GetBusInfo(int id)
        {
            var infoTemplate = GetMarkerInfoTemplate(MarkerType.Bus);
            var infoContent = busMarkerService.GetBusMarkerInfoDetails(id, infoTemplate, LoggedUser);

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
            var infoContent = rideOfferMarkerService.GetRideOfferMarkerInfoDetails(id, infoTemplate, LoggedUser);

            return infoContent;
        }

        public JsonResult UpdateRideOffer(DateTime date, TimeSpan time, string destination, int markerId)
        {
            var dateTime = date.Add(time);
            var marker = rideOfferMarkerService.Update(dateTime, destination, markerId, LoggedUser);

            return Json(marker);
        }

        public JsonResult AddHitchhiker(int markerId)
        {
            var marker = rideOfferMarkerService.AddHitchhiker(markerId, LoggedUser.Id);

            return Json(marker);
        }

        public JsonResult RemoveHitchhiker(int markerId)
        {
            var marker = rideOfferMarkerService.RemoveHitchhiker(markerId, LoggedUser.Id);

            return Json(marker);
        }

        public string GetRideRequestInfo(int id)
        {
            var infoTemplate = GetMarkerInfoTemplate(MarkerType.RideRequest);
            var infoContent = rideRequestMarkerService.GetRideRequestMarkerInfoDetails(id, infoTemplate, LoggedUser);

            return infoContent;
        }

        public JsonResult UpdateRideRequest(DateTime date, TimeSpan time, string destination, int markerId)
        {
            var dateTime = date.Add(time);
            var marker = rideRequestMarkerService.Update(dateTime, destination, markerId, LoggedUser);

            return Json(marker);
        }

        public JsonResult AddOffer(int markerId)
        {
            var marker = rideRequestMarkerService.AddOffer(markerId, LoggedUser.Id);

            return Json(marker);
        }

        public JsonResult RemoveOffer(int markerId)
        {
            var marker = rideRequestMarkerService.RemoveOffer(markerId, LoggedUser.Id);

            return Json(marker);
        }

        public string GetHelpInfo(int id)
        {
            var infoTemplate = GetMarkerInfoTemplate(MarkerType.Help);
            var infoContent = helpMarkerService.GetHelpMarkerInfoDetails(id, infoTemplate, LoggedUser);

            return infoContent;
        }

        public JsonResult UpdateHelp(int markerId, string question)
        {
            var marker = helpMarkerService.UpdateQuestion(markerId, question);

            return Json(marker);
        }

        public JsonResult AddAnswer(int markerId, string answer)
        {
            var newAnswer = new Answer()
            {
                CreatedBy = LoggedUser,
                CreatedOn = DateTime.Now,
                Text = answer
            };

            newAnswer = helpMarkerService.AddAnswer(newAnswer, markerId);

            return Json(newAnswer);
        }

        public JsonResult RemoveAnswer(int id)
        {
            helpMarkerService.RemoveAnswer(id, LoggedUser);

            return Json(null);
        }

        public JsonResult RemoveBusLine(int busLineId, int markerId)
        {
            busMarkerService.RemoveBusLine(busLineId, markerId, LoggedUser);

            return Json(null);
        }

        public JsonResult RemoveBusMarker(int markerId)
        {
            busMarkerService.Remove(markerId, LoggedUser);

            return Json(null);
        }

        public JsonResult RemoveHelpMarker(int markerId)
        {
            helpMarkerService.Remove(markerId, LoggedUser);

            return Json(null);
        }

        public JsonResult RemoveRideOfferMarker(int markerId)
        {
            rideOfferMarkerService.Remove(markerId, LoggedUser);

            return Json(null);
        }

        public JsonResult RemoveRideRequestMarker(int markerId)
        {
            rideRequestMarkerService.Remove(markerId, LoggedUser);

            return Json(null);
        }
    }
}
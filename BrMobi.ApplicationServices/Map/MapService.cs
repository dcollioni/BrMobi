using System.Collections.Generic;
using BrMobi.ApplicationServices.ServiceInterfaces.Map;
using BrMobi.Core;
using BrMobi.Core.Enums;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces.Map;
using BrMobi.Core.ViewModels.Map;

namespace BrMobi.ApplicationServices.Map
{
    public class MapService : IMapService
    {
        private readonly IBusMarkerRepository busMarkerRepository;
        private readonly IRideOfferMarkerRepository rideOfferMarkerRepository;
        private readonly IRideRequestMarkerRepository rideRequestMarkerRepository;
        private readonly IHelpMarkerRepository helpMarkerRepository;

        public MapService(IBusMarkerRepository busMarkerRepository,
                          IRideOfferMarkerRepository rideOfferMarkerRepository,
                          IRideRequestMarkerRepository rideRequestMarkerRepository,
                          IHelpMarkerRepository helpMarkerRepository)
        {
            this.busMarkerRepository = busMarkerRepository;
            this.rideOfferMarkerRepository = rideOfferMarkerRepository;
            this.rideRequestMarkerRepository = rideRequestMarkerRepository;
            this.helpMarkerRepository = helpMarkerRepository;
        }

        public BusMarker MarkBus(BusMarker busMarker)
        {
            busMarkerRepository.Create(busMarker);
            return busMarker;
        }

        public RideOfferMarker MarkRideOffer(RideOfferMarker rideOfferMarker)
        {
            rideOfferMarkerRepository.Create(rideOfferMarker);
            return rideOfferMarker;
        }

        public RideRequestMarker MarkRideRequest(RideRequestMarker rideRequestMarker)
        {
            rideRequestMarkerRepository.Create(rideRequestMarker);
            return rideRequestMarker;
        }

        public HelpMarker MarkHelp(HelpMarker helpMarker)
        {
            helpMarkerRepository.Create(helpMarker);
            return helpMarker;
        }

        public IList<MarkerViewModel> ListMarkers(LatLng southWest, LatLng northEast, User loggedUser)
        {
            var markers = new List<MarkerViewModel>();

            var busMarkers = busMarkerRepository.List(southWest, northEast);
            var rideOfferMarkers = rideOfferMarkerRepository.List(southWest, northEast, loggedUser);
            var rideRequestMarkers = rideRequestMarkerRepository.List(southWest, northEast);
            var helpMarkers = helpMarkerRepository.List(southWest, northEast);

            markers.AddRange(Convert(busMarkers));
            markers.AddRange(Convert(rideOfferMarkers));
            markers.AddRange(Convert(rideRequestMarkers));
            markers.AddRange(Convert(helpMarkers));

            return markers;
        }

        private IList<MarkerViewModel> Convert(IList<BusMarker> entities)
        {
            var viewModels = new List<MarkerViewModel>();

            foreach (var entity in entities)
            {
                viewModels.Add(new MarkerViewModel()
                {
                    Id = entity.Id,
                    ImagePath = entity.ImagePath,
                    Lat = entity.Lat,
                    Lng = entity.Lng,
                    Type = MarkerType.Bus
                });
            }

            return viewModels;
        }

        private IList<MarkerViewModel> Convert(IList<RideOfferMarker> entities)
        {
            var viewModels = new List<MarkerViewModel>();

            foreach (var entity in entities)
            {
                viewModels.Add(new MarkerViewModel()
                {
                    Id = entity.Id,
                    ImagePath = entity.ImagePath,
                    Lat = entity.Lat,
                    Lng = entity.Lng,
                    Type = MarkerType.RideOffer
                });
            }

            return viewModels;
        }

        private IList<MarkerViewModel> Convert(IList<RideRequestMarker> entities)
        {
            var viewModels = new List<MarkerViewModel>();

            foreach (var entity in entities)
            {
                viewModels.Add(new MarkerViewModel()
                {
                    Id = entity.Id,
                    ImagePath = entity.ImagePath,
                    Lat = entity.Lat,
                    Lng = entity.Lng,
                    Type = MarkerType.RideRequest
                });
            }

            return viewModels;
        }

        private IList<MarkerViewModel> Convert(IList<HelpMarker> entities)
        {
            var viewModels = new List<MarkerViewModel>();

            foreach (var entity in entities)
            {
                viewModels.Add(new MarkerViewModel()
                {
                    Id = entity.Id,
                    ImagePath = entity.ImagePath,
                    Lat = entity.Lat,
                    Lng = entity.Lng,
                    Type = MarkerType.Help
                });
            }

            return viewModels;
        }
    }
}
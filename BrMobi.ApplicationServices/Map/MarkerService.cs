using System;
using System.Collections.Generic;
using System.Linq;
using BrMobi.ApplicationServices.ServiceInterfaces.Map;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Map;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Core.ViewModels;
using BrMobi.Core.ViewModels.Map;

namespace BrMobi.ApplicationServices.Map
{
    public class MarkerService : IMarkerService
    {
        private readonly IMarkerRepository markerRepository;

        public MarkerService(IMarkerRepository markerRepository)
        {
            this.markerRepository = markerRepository;
        }

        public List<RideViewModel> List(int ownerId, int page)
        {
            return markerRepository.List(ownerId, page).Select(ToViewModel).ToList();
        }

        private static RideViewModel ToViewModel(Marker marker)
        {
            var rideViewModel = new RideViewModel
            {
                Id = marker.Id,
                Lat = marker.Lat,
                Lng = marker.Lng,
                CreatedOn = marker.CreatedOn,
                RideType = marker.GetType().Name
            };

            Func<User,UserViewModel> toViewModel = u => new UserViewModel { UserName = u.UserName };

            if (marker is RideOfferMarker)
            {
                var rideOfferMarker = marker as RideOfferMarker;
                rideViewModel.DateTime = rideOfferMarker.DateTime;
                rideViewModel.Destination = rideOfferMarker.Destination;
                rideViewModel.Stakeholders = rideOfferMarker.Hitchhikers.Select(toViewModel).ToList();
            }
            else if (marker is RideRequestMarker)
            {
                var rideRequestMarker = marker as RideRequestMarker;
                rideViewModel.DateTime = rideRequestMarker.DateTime;
                rideViewModel.Destination = rideRequestMarker.Destination;
                rideViewModel.Stakeholders = rideRequestMarker.Offers.Select(toViewModel).ToList();
            }

            return rideViewModel;
        }
        
        public void DeleteAll(int ownerId)
        {
            markerRepository.DeleteAll(ownerId);
        }
    }
}
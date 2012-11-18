using System;
using System.Globalization;
using System.Linq;
using System.Text;
using BrMobi.ApplicationServices.ServiceInterfaces.Map;
using BrMobi.Core;
using BrMobi.Core.Extensions;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Core.RepositoryInterfaces.Map;

namespace BrMobi.ApplicationServices.Map
{
    public class RideRequestMarkerService : IRideRequestMarkerService
    {
        private readonly IRideRequestMarkerRepository rideRequestMarkerRepository;
        private readonly IUserRepository userRepository;

        public RideRequestMarkerService(IUserRepository userRepository,
                                        IRideRequestMarkerRepository rideRequestMarkerRepository)
        {
            this.userRepository = userRepository;
            this.rideRequestMarkerRepository = rideRequestMarkerRepository;
        }

        public string GetRideRequestMarkerInfoDetails(int id, string template, User loggedUser)
        {
            var marker = rideRequestMarkerRepository.Get(id);

            if (marker != null)
            {
                var displayAll = (marker.Owner.Email != loggedUser.Email) ? "block" : "none";
                var displayOwner = (marker.Owner.Email != loggedUser.Email) ? "none" : "block";

                var userPicture = marker.Owner.Picture;

                var date = string.Empty;
                var time = string.Empty;

                if (marker.DateTime != DateTime.MinValue)
                {
                    date = marker.DateTime.ToString("d", new CultureInfo("pt-BR"));
                    time = marker.DateTime.ToString("t", new CultureInfo("pt-BR"));
                }

                var added = marker.Offers.Any(h => h.Id == loggedUser.Id) ? "block" : "none";
                var notAdded = added == "block" ? "none" : "block";

                var offersPictures = new StringBuilder();

                foreach (var offer in marker.Offers)
                {
                    offersPictures.AppendFormat("<a href='/Perfil/{2}'><img src='{0}' alt='Imagem do usuário' title='{1}' /></a>",
                                                    offer.Picture, offer.Name, offer.Id);
                }

                template = template.SetParam("userPicture", userPicture)
                                   .SetParam("userName", marker.Owner.Name)
                                   .SetParam("displayAll", displayAll)
                                   .SetParam("displayOwner", displayOwner)
                                   .SetParam("date", date)
                                   .SetParam("time", time)
                                   .SetParam("destination", marker.Destination)
                                   .SetParam("added", added)
                                   .SetParam("notAdded", notAdded)
                                   .SetParam("offersPictures", offersPictures.ToString())
                                   .SetParam("userId", marker.Owner.Id.ToString());
            }

            return template;
        }

        public RideRequestMarker Update(DateTime dateTime, string destination, int id, User loggedUser)
        {
            RideRequestMarker marker = null;

            if (CanUpdate(dateTime, id, loggedUser))
            {
                marker = rideRequestMarkerRepository.Update(dateTime, destination, id);
            }

            return marker;
        }

        public RideRequestMarker AddOffer(int rideRequestId, int offerId)
        {
            return rideRequestMarkerRepository.AddOffer(rideRequestId, offerId);
        }

        public RideRequestMarker RemoveOffer(int rideRequestId, int offerId)
        {
            return rideRequestMarkerRepository.RemoveOffer(rideRequestId, offerId);
        }

        private bool CanUpdate(DateTime dateTime, int markerId, User loggedUser)
        {
            var marker = rideRequestMarkerRepository.Get(markerId);

            return (
                (marker.Owner.Id == loggedUser.Id) &&
                (dateTime > DateTime.Now)
            );
        }

        public void Remove(int markerId, User loggedUser)
        {
            var marker = rideRequestMarkerRepository.Get(markerId);

            if (marker.Owner.Id == loggedUser.Id)
            {
                rideRequestMarkerRepository.Remove(markerId);
            }
        }
    }
}
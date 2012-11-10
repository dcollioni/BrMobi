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
    public class RideOfferMarkerService : IRideOfferMarkerService
    {
        private readonly IRideOfferMarkerRepository rideOfferMarkerRepository;
        private readonly IUserRepository userRepository;

        public RideOfferMarkerService(IUserRepository userRepository,
                                      IRideOfferMarkerRepository rideOfferMarkerRepository)
        {
            this.userRepository = userRepository;
            this.rideOfferMarkerRepository = rideOfferMarkerRepository;
        }

        public string GetRideOfferMarkerInfoDetails(int id, string template, User loggedUser)
        {
            var marker = rideOfferMarkerRepository.Get(id);

            if (marker != null)
            {
                var displayAll = (marker.Owner.Email != loggedUser.Email) ? "block" : "none";
                var displayOwner = (marker.Owner.Email != loggedUser.Email) ? "none" : "block";

                var userPicture = !string.IsNullOrEmpty(marker.Owner.Picture) ?
                                   string.Format("data:image/jpg;base64,{0}", marker.Owner.Picture) :
                                   "Content/Images/person.png";

                var date = string.Empty;
                var time = string.Empty;

                if (marker.DateTime != DateTime.MinValue)
                {
                    date = marker.DateTime.ToString("d", new CultureInfo("pt-BR"));
                    time = marker.DateTime.ToString("t", new CultureInfo("pt-BR"));
                }

                var added = marker.Hitchhikers.Any(h => h.Id == loggedUser.Id) ? "block" : "none";
                var notAdded = added == "block" ? "none" : "block";

                var hitchhikersPictures = new StringBuilder();

                foreach (var hitchhiker in marker.Hitchhikers)
	            {
                    hitchhikersPictures.AppendFormat("<a href='/Perfil/{2}'><img src='data:image/jpg;base64,{0}' alt='Imagem do usuário' title='{1}' /></a>",
                                                    hitchhiker.Picture, hitchhiker.Name, hitchhiker.Id);
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
                                   .SetParam("hitchhikersPictures", hitchhikersPictures.ToString())
                                   .SetParam("userId", marker.Owner.Id.ToString());
            }

            return template;
        }

        public RideOfferMarker Update(DateTime dateTime, string destination, int id, User loggedUser)
        {
            RideOfferMarker marker = null;

            if (CanUpdate(dateTime, id, loggedUser))
            {
                marker = rideOfferMarkerRepository.Update(dateTime, destination, id);
            }

            return marker;
        }

        private bool CanUpdate(DateTime dateTime, int markerId, User loggedUser)
        {
            var marker = rideOfferMarkerRepository.Get(markerId);

            return (
                (marker.Owner.Email == loggedUser.Email) &&
                (dateTime > DateTime.Now)
            );
        }

        public RideOfferMarker AddHitchhiker(int rideOfferId, int hitchhikerId)
        {
            return rideOfferMarkerRepository.AddHitchhiker(rideOfferId, hitchhikerId);
        }

        public RideOfferMarker RemoveHitchhiker(int rideOfferId, int hitchhikerId)
        {
            return rideOfferMarkerRepository.RemoveHitchhiker(rideOfferId, hitchhikerId);
        }

        public void Remove(int markerId, User loggedUser)
        {
            var marker = rideOfferMarkerRepository.Get(markerId);

            if (marker.Owner.Id == loggedUser.Id)
            {
                rideOfferMarkerRepository.Remove(markerId);
            }
        }
    }
}
using BrMobi.ApplicationServices.ServiceInterfaces.Map;
using BrMobi.Core.Extensions;
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

        public string GetRideOfferMarkerInfoDetails(int id, string template)
        {
            var marker = rideOfferMarkerRepository.Get(id);

            if (marker != null)
            {
                var userPicture = !string.IsNullOrEmpty(marker.Owner.Picture) ?
                                   string.Format("data:image/jpg;base64,{0}", marker.Owner.Picture) :
                                   "Content/Images/person.png";

                template = template.SetParam("userPicture", userPicture)
                                   .SetParam("userName", marker.Owner.Name);
            }

            return template;
        }
    }
}
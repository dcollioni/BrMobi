using System;
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Map;
using BrMobi.Core.RepositoryInterfaces.Map;

namespace BrMobi.Data.Repositories.Map
{
    public class RideRequestMarkerRepository : BaseRepository, IRideRequestMarkerRepository
    {
        public void Create(RideRequestMarker rideRequestMarker)
        {
            rideRequestMarker.Owner = Session.Query<User>(u => u.Id == rideRequestMarker.Owner.Id).SingleOrDefault();
            rideRequestMarker.Id = rideRequestMarker.GetHashCode();
            rideRequestMarker.CreatedOn = DateTime.Now;
            Session.Store(rideRequestMarker);
        }

        public IList<RideRequestMarker> List(LatLng southWest, LatLng northEast, User loggedUser)
        {
            return Session.Query<RideRequestMarker>(m => 
                southWest.Lat() <= m.Lat && m.Lat <= northEast.Lat() && southWest.Lng() <= m.Lng && m.Lng <= northEast.Lng() &&
                    ((m.Owner.Email == loggedUser.Email) || (m.Destination != null && m.DateTime >= DateTime.Now))).ToList();
        }

        public RideRequestMarker Get(int id)
        {
            return Session.Query<RideRequestMarker>(r => r.Id == id).FirstOrDefault();
        }

        public RideRequestMarker Update(DateTime dateTime, string destination, int id)
        {
            var marker = Session.Query<RideRequestMarker>(r => r.Id == id).FirstOrDefault();
            marker.DateTime = dateTime;
            marker.Destination = destination;
            Session.Store(marker);
            return marker;
        }

        public RideRequestMarker AddOffer(int rideRequestId, int offerId)
        {
            var marker = Session.Query<RideRequestMarker>(m => m.Id == rideRequestId).SingleOrDefault();

            var offer = Session.Query<User>(o => o.Id == offerId).SingleOrDefault();
            var offers = new List<User>();
            offers.AddRange(marker.Offers);
            offers.Add(offer);
            
            marker.Offers = offers;
            Session.Store(marker);

            return marker;
        }

        public RideRequestMarker RemoveOffer(int rideRequestId, int offerId)
        {
            var marker = Session.Query<RideRequestMarker>(m => m.Id == rideRequestId).SingleOrDefault();
            
            var offer = Session.Query<User>(o => o.Id == offerId).SingleOrDefault();
            var offers = new List<User>();
            offers.AddRange(marker.Offers);
            offers.Remove(offer);
            
            marker.Offers = offers;
            Session.Store(marker);
            return marker;
        }

        public void Remove(int markerId)
        {
            var marker = Session.Query<RideRequestMarker>(m => m.Id == markerId).SingleOrDefault();
            Session.Delete(marker);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Map;
using BrMobi.Core.RepositoryInterfaces.Map;

namespace BrMobi.Data.Repositories.Map
{
    public class RideOfferMarkerRepository : BaseRepository, IRideOfferMarkerRepository
    {
        public void Create(RideOfferMarker rideOfferMarker)
        {
            rideOfferMarker.Owner = Session.Query<User>(u => u.Email == rideOfferMarker.Owner.Email).SingleOrDefault();
            rideOfferMarker.Id = rideOfferMarker.GetHashCode();
            rideOfferMarker.CreatedOn = DateTime.Now;
            Session.Store(rideOfferMarker);
        }

        public IList<RideOfferMarker> List(LatLng southWest, LatLng northEast, User loggedUser)
        {
            return Session.Query<RideOfferMarker>(m => 
                southWest.Lat() <= m.Lat && m.Lat <= northEast.Lat() && southWest.Lng() <= m.Lng && m.Lng <= northEast.Lng() &&
                    ((m.Owner.Email == loggedUser.Email) || (m.Destination != null && m.DateTime >= DateTime.Now))).ToList();
        }

        public RideOfferMarker Get(int id)
        {
            return Session.Query<RideOfferMarker>(r => r.Id == id).FirstOrDefault();
        }

        public RideOfferMarker Update(DateTime dateTime, string destination, int id)
        {
            var marker = Session.Query<RideOfferMarker>(r => r.Id == id).FirstOrDefault();
            marker.DateTime = dateTime;
            marker.Destination = destination;
            Session.Store(marker);
            return marker;
        }

        public RideOfferMarker AddHitchhiker(int rideOfferId, int hitchhikerId)
        {
            var marker = Session.Query<RideOfferMarker>(m => m.Id == rideOfferId).SingleOrDefault();

            var hitchhiker = Session.Query<User>(h => h.Id == hitchhikerId).SingleOrDefault();

            var hitchhikers = new List<User>();
            hitchhikers.AddRange(marker.Hitchhikers);
            hitchhikers.Add(hitchhiker);

            marker.Hitchhikers = hitchhikers;
            Session.Store(marker);

            return marker;
        }

        public RideOfferMarker RemoveHitchhiker(int rideOfferId, int hitchhikerId)
        {
            var marker = Session.Query<RideOfferMarker>(m => m.Id == rideOfferId).SingleOrDefault();
                
            var hitchhiker = Session.Query<User>(h => h.Id == hitchhikerId).SingleOrDefault();
            var hitchhikers = new List<User>();
            hitchhikers.AddRange(marker.Hitchhikers);
            hitchhikers.Remove(hitchhiker);

            marker.Hitchhikers = hitchhikers;
            Session.Store(marker);
            return marker;
        }

        public void Remove(int markerId)
        {
            var marker = Session.Query<RideOfferMarker>(m => m.Id == markerId).SingleOrDefault();
            Session.Delete(marker);
        }
    }
}
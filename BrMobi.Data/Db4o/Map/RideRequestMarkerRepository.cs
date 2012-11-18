using System;
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces.Map;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o.Map
{
    public class RideRequestMarkerRepository : BaseRepository, IRideRequestMarkerRepository
    {
        public void Create(RideRequestMarker rideRequestMarker)
        {
            //using (var server = Server)
            //{
                using (var client = Client)
                {
                    rideRequestMarker.Owner = client.Query<User>(u => u.Email == rideRequestMarker.Owner.Email).SingleOrDefault();
                    rideRequestMarker.Id = rideRequestMarker.GetHashCode();
                    rideRequestMarker.CreatedOn = DateTime.Now;
                    client.Store(rideRequestMarker);
                }
            //}
        }

        public IList<RideRequestMarker> List(LatLng southWest, LatLng northEast, User loggedUser)
        {
            var markers = new List<RideRequestMarker>();

            //using (var server = Server)
            //{
                using (var client = Client)
                {
                    markers = client.Query<RideRequestMarker>(m => southWest.Lat() <= m.Lat && m.Lat <= northEast.Lat() &&
                                                                 southWest.Lng() <= m.Lng && m.Lng <= northEast.Lng() &&
                                                                 (
                                                                    (m.Owner.Email == loggedUser.Email) ||
                                                                    (m.Destination != null && m.DateTime >= DateTime.Now)
                                                                 ))
                                                                 .ToList();
                }
            //}

            return markers;
        }

        public RideRequestMarker Get(int id)
        {
            RideRequestMarker marker;

            //using (var server = Server)
            //{
                using (var client = Client)
                {
                    marker = client.Query<RideRequestMarker>(r => r.Id == id).FirstOrDefault();
                }
            //}

            return marker;
        }

        public RideRequestMarker Update(DateTime dateTime, string destination, int id)
        {
            RideRequestMarker marker;

            //using (var server = Server)
            //{
                using (var client = Client)
                {
                    marker = client.Query<RideRequestMarker>(r => r.Id == id).FirstOrDefault();
                    marker.DateTime = dateTime;
                    marker.Destination = destination;
                    client.Store(marker);
                }
            //}

            return marker;
        }

        public RideRequestMarker AddOffer(int rideRequestId, int offerId)
        {
            RideRequestMarker marker = null;

            //using (var server = Server)
            //{
                using (var client = Client)
                {
                    marker = client.Query<RideRequestMarker>(m => m.Id == rideRequestId).SingleOrDefault();

                    var offer = client.Query<User>(o => o.Id == offerId).SingleOrDefault();

                    var offers = new List<User>();
                    offers.AddRange(marker.Offers);
                    offers.Add(offer);

                    marker.Offers = offers;
                    client.Store(marker);
                }
            //}

            return marker;
        }

        public RideRequestMarker RemoveOffer(int rideRequestId, int offerId)
        {
            RideRequestMarker marker = null;

            //using (var server = Server)
            //{
                using (var client = Client)
                {
                    marker = client.Query<RideRequestMarker>(m => m.Id == rideRequestId).SingleOrDefault();

                    var offer = client.Query<User>(o => o.Id == offerId).SingleOrDefault();

                    var offers = new List<User>();
                    offers.AddRange(marker.Offers);
                    offers.Remove(offer);

                    marker.Offers = offers;
                    client.Store(marker);
                }
            //}

            return marker;
        }

        public void Remove(int markerId)
        {
            //using (var server = Server)
            //{
                using (var client = Client)
                {
                    var marker = client.Query<RideRequestMarker>(m => m.Id == markerId).SingleOrDefault();

                    client.Delete(marker);
                }
            //}
        }
    }
}
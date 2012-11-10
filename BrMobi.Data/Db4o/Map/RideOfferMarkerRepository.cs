using System;
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces.Map;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o.Map
{
    public class RideOfferMarkerRepository : BaseRepository, IRideOfferMarkerRepository
    {
        public void Create(RideOfferMarker rideOfferMarker)
        {
            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    rideOfferMarker.Owner = client.Query<User>(u => u.Email == rideOfferMarker.Owner.Email).SingleOrDefault();
                    rideOfferMarker.Id = rideOfferMarker.GetHashCode();
                    rideOfferMarker.CreatedOn = DateTime.Now;
                    client.Store(rideOfferMarker);
                }
            }
        }

        public IList<RideOfferMarker> List(LatLng southWest, LatLng northEast, User loggedUser)
        {
            var markers = new List<RideOfferMarker>();

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    markers = client.Query<RideOfferMarker>(m => southWest.Lat() <= m.Lat && m.Lat <= northEast.Lat() &&
                                                                 southWest.Lng() <= m.Lng && m.Lng <= northEast.Lng() &&
                                                                 (
                                                                    (m.Owner.Email == loggedUser.Email) ||
                                                                    (m.Destination != null && m.DateTime >= DateTime.Now)
                                                                 ))
                                                                 .ToList();
                }
            }

            return markers;
        }

        public RideOfferMarker Get(int id)
        {
            RideOfferMarker marker;

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    marker = client.Query<RideOfferMarker>(r => r.Id == id).FirstOrDefault();
                }
            }

            return marker;
        }

        public RideOfferMarker Update(DateTime dateTime, string destination, int id)
        {
            RideOfferMarker marker;

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    marker = client.Query<RideOfferMarker>(r => r.Id == id).FirstOrDefault();
                    marker.DateTime = dateTime;
                    marker.Destination = destination;
                    client.Store(marker);
                }
            }

            return marker;
        }

        public RideOfferMarker AddHitchhiker(int rideOfferId, int hitchhikerId)
        {
            RideOfferMarker marker = null;

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    marker = client.Query<RideOfferMarker>(m => m.Id == rideOfferId).SingleOrDefault();

                    var hitchhiker = client.Query<User>(h => h.Id == hitchhikerId).SingleOrDefault();

                    var hitchhikers = new List<User>();
                    hitchhikers.AddRange(marker.Hitchhikers);
                    hitchhikers.Add(hitchhiker);

                    marker.Hitchhikers = hitchhikers;
                    client.Store(marker);
                }
            }

            return marker;
        }

        public RideOfferMarker RemoveHitchhiker(int rideOfferId, int hitchhikerId)
        {
            RideOfferMarker marker = null;

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    marker = client.Query<RideOfferMarker>(m => m.Id == rideOfferId).SingleOrDefault();

                    var hitchhiker = client.Query<User>(h => h.Id == hitchhikerId).SingleOrDefault();

                    var hitchhikers = new List<User>();
                    hitchhikers.AddRange(marker.Hitchhikers);
                    hitchhikers.Remove(hitchhiker);

                    marker.Hitchhikers = hitchhikers;
                    client.Store(marker);
                }
            }

            return marker;
        }

        public void Remove(int markerId)
        {
            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    var marker = client.Query<RideOfferMarker>(m => m.Id == markerId).SingleOrDefault();

                    client.Delete(marker);
                }
            }
        }
    }
}
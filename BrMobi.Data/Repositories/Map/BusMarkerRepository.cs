using System;
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Map;
using BrMobi.Core.RepositoryInterfaces.Map;

namespace BrMobi.Data.Repositories.Map
{
    public class BusMarkerRepository : BaseRepository, IBusMarkerRepository
    {
        public void Create(BusMarker busMarker)
        {
            busMarker.Owner = Session.Query<User>(u => u.Email == busMarker.Owner.Email).SingleOrDefault();
            busMarker.Id = busMarker.GetHashCode();
            busMarker.CreatedOn = DateTime.Now;
            Session.Store(busMarker);
        }

        public IList<BusMarker> List(LatLng southWest, LatLng northEast)
        {
            return Session.Query<BusMarker>(m => 
                southWest.Lat() <= m.Lat && m.Lat <= northEast.Lat() && southWest.Lng() <= m.Lng && m.Lng <= northEast.Lng()).ToList();
        }

        public BusMarker AddLine(int busMarkerId, int busLineId)
        {
            var busMarker = Session.Query<BusMarker>(b => b.Id == busMarkerId).SingleOrDefault();
            var busLine = Session.Query<BusLine>(b => b.Id == busLineId).SingleOrDefault();
            
            var lines = new List<BusLine>();
            lines.AddRange(busMarker.Lines);
            lines.Add(busLine);

            busMarker.Lines = lines;
            Session.Store(busMarker);
            return busMarker;
        }

        public BusMarker Get(int id)
        {
            return Session.Query<BusMarker>(b => b.Id == id).SingleOrDefault();
        }

        public IList<BusLine> ListBusLines(int busMarkerId)
        {
            var busLines = new List<BusLine>();
            var busMarker = Session.Query<BusMarker>(b => b.Id == busMarkerId).SingleOrDefault();
            if (busMarker != null)
            {
                busLines = busMarker.Lines.OrderBy(l => l.Name).ToList();
            }
            return busLines;
        }

        public void RemoveBusLine(int busLineId, int markerId)
        {
            var marker = Session.Query<BusMarker>(m => m.Id == markerId).SingleOrDefault();

            if (marker != null)
            {
                var line = Session.Query<BusLine>(l => l.Id == busLineId).SingleOrDefault();

                var lines = new List<BusLine>();
                lines.AddRange(marker.Lines);
                lines.Remove(line);

                marker.Lines = lines;
                Session.Store(marker);
            }
        }

        public void Remove(int markerId)
        {
            var marker = Session.Query<BusMarker>(m => m.Id == markerId).SingleOrDefault();
            Session.Delete(marker);
        }
    }
}
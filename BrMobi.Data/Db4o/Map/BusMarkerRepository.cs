using System;
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces.Map;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o.Map
{
    public class BusMarkerRepository : BaseRepository, IBusMarkerRepository
    {
        public BusMarkerRepository(Db4objects.Db4o.IObjectServer server)
            : base(server)
        {
        }

        public void Create(BusMarker busMarker)
        {
            using (var client = Server.OpenClient())
            {
                busMarker.Owner = client.Query<User>(u => u.Email == busMarker.Owner.Email).SingleOrDefault();
                busMarker.Id = busMarker.GetHashCode();
                busMarker.CreatedOn = DateTime.Now;
                client.Store(busMarker);
            }
        }

        public IList<BusMarker> List(LatLng southWest, LatLng northEast)
        {
            var markers = new List<BusMarker>();

            using (var client = Server.OpenClient())
            {
                markers = client.Query<BusMarker>(m => southWest.Lat() <= m.Lat && m.Lat <= northEast.Lat()
                                                    && southWest.Lng() <= m.Lng && m.Lng <= northEast.Lng()).ToList();
            }

            return markers;
        }

        public BusMarker AddLine(int busMarkerId, int busLineId)
        {
            BusMarker busMarker = null;

            using (var client = Server.OpenClient())
            {
                busMarker = client.Query<BusMarker>(b => b.Id == busMarkerId).SingleOrDefault();

                var busLine = client.Query<BusLine>(b => b.Id == busLineId).SingleOrDefault();

                var lines = new List<BusLine>();
                lines.AddRange(busMarker.Lines);
                lines.Add(busLine);

                busMarker.Lines = lines;
                client.Store(busMarker);
            }

            return busMarker;
        }

        public BusMarker Get(int id)
        {
            BusMarker busMarker = null;

            using (var client = Server.OpenClient())
            {
                busMarker = client.Query<BusMarker>(b => b.Id == id).SingleOrDefault();
            }

            return busMarker;
        }

        public IList<BusLine> ListBusLines(int busMarkerId)
        {
            var busLines = new List<BusLine>();

            using (var client = Server.OpenClient())
            {
                var busMarker = client.Query<BusMarker>(b => b.Id == busMarkerId).SingleOrDefault();

                if (busMarker != null)
                {
                    busLines = busMarker.Lines.OrderBy(l => l.Name).ToList();
                }
            }

            return busLines;
        }

        public void RemoveBusLine(int busLineId, int markerId)
        {
            using (var client = Server.OpenClient())
            {
                var marker = client.Query<BusMarker>(m => m.Id == markerId).SingleOrDefault();

                if (marker != null)
                {
                    var line = client.Query<BusLine>(l => l.Id == busLineId).SingleOrDefault();

                    var lines = new List<BusLine>();
                    lines.AddRange(marker.Lines);
                    lines.Remove(line);

                    marker.Lines = lines;
                    client.Store(marker);
                }
            }
        }

        public void Remove(int markerId)
        {
            using (var client = Server.OpenClient())
            {
                var marker = client.Query<BusMarker>(m => m.Id == markerId).SingleOrDefault();
                client.Delete(marker);
            }
        }
    }
}
using System;
using System.Collections.Generic;

namespace BrMobi.Core.Map
{
    public class RideOfferMarker : Marker
    {
        public override int Id { get; set; }
        public override double Lat { get; set; }
        public override double Lng { get; set; }
        public override User Owner { get; set; }
        public override string ImagePath { get; set; }
        public DateTime DateTime { get; set; }
        public string Destination { get; set; }
        public IList<User> Hitchhikers { get; set; }

        public RideOfferMarker()
            : base()
        {
            Hitchhikers = new List<User>();
        }

        public RideOfferMarker(double lat, double lng, User owner, string imagePath)
            : base(lat, lng, owner, imagePath)
        {
            Hitchhikers = new List<User>();
        }
    }
}
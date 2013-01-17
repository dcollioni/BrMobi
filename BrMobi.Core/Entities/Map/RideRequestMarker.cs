using System;
using System.Collections.Generic;

namespace BrMobi.Core.Entities.Map
{
    public class RideRequestMarker : Marker
    {
        public override int Id { get; set; }
        public override double Lat { get; set; }
        public override double Lng { get; set; }
        public override User Owner { get; set; }
        public override string ImagePath { get; set; }
        public override DateTime CreatedOn { get; set; }
        public DateTime DateTime { get; set; }
        public string Destination { get; set; }
        public IList<User> Offers { get; set; }

        public RideRequestMarker()
            : base()
        {
            Offers = new List<User>();
        }

        public RideRequestMarker(double lat, double lng, User owner, string imagePath)
            : base(lat, lng, owner, imagePath)
        {
            Offers = new List<User>();
        }
    }
}
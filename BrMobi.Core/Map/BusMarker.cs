using System;
using System.Collections.Generic;

namespace BrMobi.Core.Map
{
    public class BusMarker : Marker
    {
        public override int Id { get; set; }
        public override double Lat { get; set; }
        public override double Lng { get; set; }
        public override User Owner { get; set; }
        public override string ImagePath { get; set; }
        public override DateTime CreatedOn { get; set; }
        public IList<BusLine> Lines { get; set; }

        public BusMarker()
            : base()
        {
            Lines = new List<BusLine>();
        }

        public BusMarker(double lat, double lng, User owner, string imagePath)
            : base(lat, lng, owner, imagePath)
        {
            Lines = new List<BusLine>();
        }
    }
}
﻿using System.IO;

namespace BrMobi.Core.Map
{
    public class HelpMarker : Marker
    {
        public override int Id { get; set; }
        public override double Lat { get; set; }
        public override double Lng { get; set; }
        public override User Owner { get; set; }
        public override string ImagePath { get; set; }

        public HelpMarker()
            : base()
        {
        }

        public HelpMarker(double lat, double lng, User owner, string imagePath)
            : base(lat, lng, owner, imagePath)
        {
        }
    }
}
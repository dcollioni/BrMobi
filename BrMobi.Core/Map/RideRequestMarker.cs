using System.IO;

namespace BrMobi.Core.Map
{
    public class RideRequestMarker : Marker
    {
        public override int Id { get; set; }
        public override double Lat { get; set; }
        public override double Lng { get; set; }
        public override User Owner { get; set; }
        public override string ImagePath { get; set; }

        public RideRequestMarker()
            : base()
        {
        }

        public RideRequestMarker(double lat, double lng, User owner, string imagePath)
            : base(lat, lng, owner, imagePath)
        {
        }
    }
}
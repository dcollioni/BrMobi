namespace BrMobi.Core.Map
{
    public abstract class Marker
    {
        public abstract int Id { get; set; }
        public abstract double Lat { get; set; }
        public abstract double Lng { get; set; }
        public abstract User Owner { get; set; }
        public abstract string ImagePath { get; set; }

        public Marker()
        {
        }

        public Marker(double lat, double lng, User owner, string imagePath)
        {
            Lat = lat;
            Lng = lng;
            Owner = owner;
            ImagePath = imagePath;
        }
    }
}
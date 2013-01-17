namespace BrMobi.Core.Entities.Map
{
    public class LatLng
    {
        public double Xa { get; set; }
        public double Ya { get; set; }

        public LatLng()
        {
        }

        public LatLng(double xa, double ya)
        {
            Xa = xa;
            Ya = ya;
        }

        public double Lat()
        {
            return Xa;
        }

        public double Lng()
        {
            return Ya;
        }
    }
}
using BrMobi.Core.Enums;

namespace BrMobi.Core.ViewModels.Map
{
    public class MarkerViewModel
    {
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string ImagePath { get; set; }
        public MarkerType Type { get; set; }
    }
}
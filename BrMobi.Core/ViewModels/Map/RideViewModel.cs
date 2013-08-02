using System;
using System.Collections.Generic;

namespace BrMobi.Core.ViewModels.Map
{
    public class RideViewModel
    {
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Destination { get; set; }
        public IList<UserViewModel> Stakeholders { get; set; }
        public string RideType { get; set; }
    }
}
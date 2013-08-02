using System;

namespace BrMobi.Core.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string FacebookLink { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string UserName { get; set; }
    }
}
using System;
using BrMobi.Core.Enums;

namespace BrMobi.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Picture { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string FacebookLink { get; set; }
        public City City { get; set; }

        public string FacebookId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Link { get; set; }
        public string UserName { get; set; }
        public string Locale { get; set; }
        public string AgeRange { get; set; }
    }
}
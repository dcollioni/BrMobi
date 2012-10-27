using System;

namespace BrMobi.Core
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Picture { get; set; }
    }
}
using System;

namespace BrMobi.Core.Entities
{
    /// <summary>
    /// Entity used to save the emails from landing page.
    /// </summary>
    public class PreUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredOn { get; set; }

        public PreUser(DateTime registeredOn)
        {
            RegisteredOn = registeredOn;
        }
    }
}
using System;

namespace BrMobi.Core
{
    public class Message
    {
        public int Id { get; set; }
        public User From { get; set; }
        public User To { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
using System;

namespace BrMobi.Core.Map
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public HelpMarker Marker { get; set; }
    }
}
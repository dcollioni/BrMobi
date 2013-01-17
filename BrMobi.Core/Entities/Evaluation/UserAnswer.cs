using System;

namespace BrMobi.Core.Entities.Evaluation
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public DateTime AnsweredOn { get; set; }
        public User AnsweredBy { get; set; }
        public Question Question { get; set; }
        public int Value { get; set; }
    }
}
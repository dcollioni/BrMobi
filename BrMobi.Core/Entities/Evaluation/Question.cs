using BrMobi.Core.Enums.Evaluation;

namespace BrMobi.Core.Entities.Evaluation
{
    public class Question
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public QuestionCategory Category { get; set; }

        public Question()
        {
        }

        public Question(int id, string description, QuestionCategory category)
        {
            Id = id;
            Description = description;
            Category = category;
        }
    }
}
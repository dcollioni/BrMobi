using BrMobi.Core.Evaluation;

namespace BrMobi.Core.RepositoryInterfaces.Evaluation
{
    public interface IUserAnswerRepository
    {
        UserAnswer Create(UserAnswer userAnswer);
        int CountUserAnswers(User user);
    }
}
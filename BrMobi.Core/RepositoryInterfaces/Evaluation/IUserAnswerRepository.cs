using System.Collections.Generic;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Evaluation;

namespace BrMobi.Core.RepositoryInterfaces.Evaluation
{
    public interface IUserAnswerRepository
    {
        UserAnswer Create(UserAnswer userAnswer);
        int CountUserAnswers(User user);
        IList<UserAnswer> ListAll();
    }
}
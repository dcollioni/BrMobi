using System.Collections.Generic;
using BrMobi.Core.Evaluation;

namespace BrMobi.Core.RepositoryInterfaces.Evaluation
{
    public interface IQuestionRepository
    {
        IList<Question> ListAll();
        Question Get(int id);
    }
}
using System.Collections.Generic;
using BrMobi.Core.Entities.Evaluation;

namespace BrMobi.Core.RepositoryInterfaces.Evaluation
{
    public interface IQuestionRepository
    {
        IList<Question> ListAll();
        Question Get(int id);
    }
}
using System.Collections.Generic;
using BrMobi.Core.Evaluation;

namespace BrMobi.ApplicationServices.ServiceInterfaces.Evaluation
{
    public interface IQuestionService
    {
        IList<Question> ListAll();
    }
}
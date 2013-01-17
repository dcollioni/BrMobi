using System.Collections.Generic;
using BrMobi.Core;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Evaluation;

namespace BrMobi.ApplicationServices.ServiceInterfaces.Evaluation
{
    public interface IEvaluationService
    {
        void SaveUserAnswers(IDictionary<int, int> answers, User loggedUser);
        bool CanEvaluate(User loggedUser);
        IList<UserAnswer> ListAllUserAnswers();
    }
}
using System.Collections.Generic;
using BrMobi.Core;

namespace BrMobi.ApplicationServices.ServiceInterfaces.Evaluation
{
    public interface IEvaluationService
    {
        void SaveUserAnswers(IDictionary<int, int> answers, User loggedUser);
        bool CanEvaluate(User loggedUser);
    }
}
using System;
using System.Collections.Generic;
using BrMobi.ApplicationServices.ServiceInterfaces.Evaluation;
using BrMobi.Core;
using BrMobi.Core.Evaluation;
using BrMobi.Core.RepositoryInterfaces.Evaluation;

namespace BrMobi.ApplicationServices.Evaluation
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IUserAnswerRepository userAnswerRepository;
        private readonly IQuestionRepository questionRepository;

        public EvaluationService(IUserAnswerRepository userAnswerRepository,
                                 IQuestionRepository questionRepository)
        {
            this.userAnswerRepository = userAnswerRepository;
            this.questionRepository = questionRepository;
        }

        public void SaveUserAnswers(IDictionary<int, int> answers, User loggedUser)
        {
            foreach (var answer in answers)
            {
                var userAnswer = new UserAnswer()
                {
                    AnsweredBy = loggedUser,
                    AnsweredOn = DateTime.Now,
                    Question = questionRepository.Get(answer.Key),
                    Value = answer.Value
                };

                userAnswerRepository.Create(userAnswer);
            }
        }

        public bool CanEvaluate(User loggedUser)
        {
            return userAnswerRepository.CountUserAnswers(loggedUser) == 0;
        }
    }
}
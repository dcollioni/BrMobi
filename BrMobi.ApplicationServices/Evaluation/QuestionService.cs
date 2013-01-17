using System.Collections.Generic;
using BrMobi.ApplicationServices.ServiceInterfaces.Evaluation;
using BrMobi.Core.Entities.Evaluation;
using BrMobi.Core.RepositoryInterfaces.Evaluation;

namespace BrMobi.ApplicationServices.Evaluation
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public IList<Question> ListAll()
        {
            return questionRepository.ListAll();
        }
    }
}
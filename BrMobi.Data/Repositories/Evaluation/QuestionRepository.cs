using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Entities.Evaluation;
using BrMobi.Core.RepositoryInterfaces.Evaluation;

namespace BrMobi.Data.Repositories.Evaluation
{
    public class QuestionRepository : BaseRepository, IQuestionRepository
    {
        public IList<Question> ListAll()
        {
            return Session.Query<Question>().OrderBy(s => s.Id).ToList();
        }

        public Question Get(int id)
        {
            return Session.Query<Question>(q => q.Id == id).SingleOrDefault();
        }
    }
}
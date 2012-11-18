using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Evaluation;
using BrMobi.Core.RepositoryInterfaces.Evaluation;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o.Evaluation
{
    public class QuestionRepository : BaseRepository, IQuestionRepository
    {
        private readonly Db4objects.Db4o.IObjectServer server;

        public QuestionRepository(Db4objects.Db4o.IObjectServer server)
        {
            this.server = server;
        }

        public IList<Question> ListAll()
        {
            var questions = new List<Question>();

            //using (var server = Server)
            //{
                using (var client = server.OpenClient())
                {
                    questions = client.Query<Question>().OrderBy(s => s.Id).ToList();
                }
            //}

            return questions;
        }

        public Question Get(int id)
        {
            Question question = null;

            //using (var server = Server)
            //{
                using (var client = server.OpenClient())
                {
                    question = client.Query<Question>(q => q.Id == id).SingleOrDefault();
                }
            //}

            return question;
        }
    }
}
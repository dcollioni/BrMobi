using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.Evaluation;
using BrMobi.Core.RepositoryInterfaces.Evaluation;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o.Evaluation
{
    public class UserAnswerRepository : BaseRepository, IUserAnswerRepository
    {
        private readonly Db4objects.Db4o.IObjectServer server;

        public UserAnswerRepository(Db4objects.Db4o.IObjectServer server)
        {
            this.server = server;
        }

        public UserAnswer Create(UserAnswer userAnswer)
        {
            //using (var server = Server)
            //{
                using (var client = server.OpenClient())
                {
                    userAnswer.AnsweredBy = client.Query<User>(u => u.Id == userAnswer.AnsweredBy.Id).SingleOrDefault();
                    userAnswer.Question = client.Query<Question>(q => q.Id == userAnswer.Question.Id).SingleOrDefault();
                    userAnswer.Id = userAnswer.GetHashCode();

                    client.Store(userAnswer);
                }
            //}

            return userAnswer;
        }

        public int CountUserAnswers(User user)
        {
            var count = 0;

            using (var client = server.OpenClient())
            {
                count = client.Query<UserAnswer>(u => u.AnsweredBy.Id == user.Id).Count;
            }

            return count;
        }

        public IList<UserAnswer> ListAll()
        {
            var userAnswers = new List<UserAnswer>();

            using (var client = server.OpenClient())
            {
                userAnswers = client.Query<UserAnswer>().ToList();
            }

            return userAnswers;
        }
    }
}
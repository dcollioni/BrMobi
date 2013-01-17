using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Evaluation;
using BrMobi.Core.RepositoryInterfaces.Evaluation;

namespace BrMobi.Data.Repositories.Evaluation
{
    public class UserAnswerRepository : BaseRepository, IUserAnswerRepository
    {
        public UserAnswer Create(UserAnswer userAnswer)
        {
            userAnswer.AnsweredBy = Session.Query<User>(u => u.Id == userAnswer.AnsweredBy.Id).SingleOrDefault();
            userAnswer.Question = Session.Query<Question>(q => q.Id == userAnswer.Question.Id).SingleOrDefault();
            userAnswer.Id = userAnswer.GetHashCode();
            Session.Store(userAnswer);
            return userAnswer;
        }

        public int CountUserAnswers(User user)
        {
            return Session.Query<UserAnswer>(u => u.AnsweredBy.Id == user.Id).Count;
        }

        public IList<UserAnswer> ListAll()
        {
            return Session.Query<UserAnswer>().ToList();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Entities.Map;
using BrMobi.Core.RepositoryInterfaces.Map;

namespace BrMobi.Data.Repositories.Map
{
    public class AnswerRepository : BaseRepository, IAnswerRepository
    {
        public Answer Get(int id)
        {
            return Session.Query<Answer>(a => a.Id == id).SingleOrDefault();
        }

        public IList<Answer> List(int markerId)
        {
            return Session.Query<Answer>(a => a.Marker.Id == markerId).ToList();
        }
    }
}
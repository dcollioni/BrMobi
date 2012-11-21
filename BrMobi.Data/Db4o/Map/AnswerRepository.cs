using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces.Map;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o.Map
{
    public class AnswerRepository : BaseRepository, IAnswerRepository
    {
        public AnswerRepository(Db4objects.Db4o.IObjectServer server)
            : base(server)
        {
        }

        public Answer Get(int id)
        {
            Answer answer = null;

            using (var client = Server.OpenClient())
            {
                answer = client.Query<Answer>(a => a.Id == id).SingleOrDefault();
            }

            return answer;
        }

        public IList<Answer> List(int markerId)
        {
            IList<Answer> answers = new List<Answer>();

            using (var client = Server.OpenClient())
            {
                answers = client.Query<Answer>(a => a.Marker.Id == markerId).ToList();
            }

            return answers;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces.Map;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o.Map
{
    public class AnswerRepository : BaseRepository, IAnswerRepository
    {
        private readonly Db4objects.Db4o.IObjectServer server;

        public AnswerRepository(Db4objects.Db4o.IObjectServer server)
        {
            this.server = server;
        }

        public Answer Get(int id)
        {
            Answer answer = null;

            //using (var server = Server)
            //{
                using (var client = server.OpenClient())
                {
                    answer = client.Query<Answer>(a => a.Id == id).SingleOrDefault();
                }
            //}

            return answer;
        }

        public IList<Answer> List(int markerId)
        {
            IList<Answer> answers = new List<Answer>();

            //using (var server = Server)
            //{
                using (var client = server.OpenClient())
                {
                    answers = client.Query<Answer>(a => a.Marker.Id == markerId).ToList();
                }
            //}

            return answers;
        }
    }
}
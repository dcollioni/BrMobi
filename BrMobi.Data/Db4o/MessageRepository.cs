using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(Db4objects.Db4o.IObjectServer server)
            : base(server)
        {
        }

        public Message Create(Message message)
        {
            using (var client = Server.OpenClient())
            {
                message.Id = message.GetHashCode();

                message.From = client.Query<User>(u => u.Id == message.From.Id).SingleOrDefault();
                message.To = client.Query<User>(u => u.Id == message.To.Id).SingleOrDefault();

                client.Store(message);
            }

            return message;
        }

        public IList<Message> ListReceived(int userId)
        {
            var messages = new List<Message>();

            using (var client = Server.OpenClient())
            {
                messages = client.Query<Message>(m => m.To.Id == userId)
                                 .OrderByDescending(m => m.CreatedOn).ToList();
            }

            return messages;
        }

        public void Remove(int id)
        {
            using (var client = Server.OpenClient())
            {
                var message = client.Query<Message>(m => m.Id == id).SingleOrDefault();
                client.Delete(message);
            }
        }

        public Message Get(int id)
        {
            Message message = null;

            using (var client = Server.OpenClient())
            {
                message = client.Query<Message>(m => m.Id == id).SingleOrDefault();
            }

            return message;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public Message Create(Message message)
        {
            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    message.Id = message.GetHashCode();

                    message.From = client.Query<User>(u => u.Id == message.From.Id).SingleOrDefault();
                    message.To = client.Query<User>(u => u.Id == message.To.Id).SingleOrDefault();

                    client.Store(message);
                }
            }

            return message;
        }

        public IList<Message> ListReceived(int userId)
        {
            var messages = new List<Message>();

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    messages = client.Query<Message>(m => m.To.Id == userId)
                                     .OrderByDescending(m => m.CreatedOn).ToList();
                }
            }

            return messages;
        }

        public void Remove(int id)
        {
            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    var message = client.Query<Message>(m => m.Id == id).SingleOrDefault();
                    client.Delete(message);
                }
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.Entities;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Data.Repositories;

namespace BrMobi.Data.Repositories
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public Message Create(Message message)
        {
            message.Id = message.GetHashCode();
            message.From = Session.Query<User>(u => u.Id == message.From.Id).SingleOrDefault();
            message.To = Session.Query<User>(u => u.Id == message.To.Id).SingleOrDefault();
            Session.Store(message);
            return message;
        }

        public IList<Message> ListReceived(int userId)
        {
            return Session.Query<Message>(m => m.To.Id == userId)
                            .OrderByDescending(m => m.CreatedOn).ToList();
        }

        public void Remove(int id)
        {
            var message = Session.Query<Message>(m => m.Id == id).SingleOrDefault();
            Session.Delete(message);
        }

        public Message Get(int id)
        {
            return Session.Query<Message>(m => m.Id == id).SingleOrDefault();
        }
    }
}
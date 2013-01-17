using System.Collections.Generic;
using BrMobi.Core.Entities;

namespace BrMobi.Core.RepositoryInterfaces
{
    public interface IMessageRepository
    {
        Message Create(Message message);
        void Remove(int id);
        IList<Message> ListReceived(int userId);
        Message Get(int id);
    }
}
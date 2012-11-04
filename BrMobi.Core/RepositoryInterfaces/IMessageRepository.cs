using System.Collections.Generic;

namespace BrMobi.Core.RepositoryInterfaces
{
    public interface IMessageRepository
    {
        Message Create(Message message);
        void Remove(int id);
        IList<Message> ListReceived(int userId);
    }
}
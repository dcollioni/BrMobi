using System.Collections.Generic;
using BrMobi.Core;

namespace BrMobi.ApplicationServices.ServiceInterfaces
{
    public interface IMessageService
    {
        Message Create(Message message);
        void Remove(int id);
        
        /// <summary>
        /// Lists the messages received by the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The list of messages.</returns>
        IList<Message> ListReceived(int userId);
    }
}
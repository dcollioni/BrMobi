using System.Collections.Generic;
using BrMobi.ApplicationServices.ServiceInterfaces;
using BrMobi.Core;
using BrMobi.Core.Entities;
using BrMobi.Core.RepositoryInterfaces;

namespace BrMobi.ApplicationServices
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public Message Create(Message message)
        {
            return messageRepository.Create(message);
        }

        public IList<Message> ListReceived(int userId)
        {
            return messageRepository.ListReceived(userId);
        }

        public void Remove(int id, User loggedUser)
        {
            var message = messageRepository.Get(id);

            if (message.From.Id == loggedUser.Id || message.To.Id == loggedUser.Id)
            {
                messageRepository.Remove(id);
            }
        }
    }
}
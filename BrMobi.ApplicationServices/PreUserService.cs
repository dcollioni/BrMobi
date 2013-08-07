using System.Collections.Generic;
using System.Net.Mail;
using BrMobi.ApplicationServices.ServiceInterfaces;
using BrMobi.Core.Entities;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Core.Resources;
using BrMobi.Service.ApplicationServices;

namespace BrMobi.ApplicationServices
{
    public class PreUserService : BaseService, IPreUserService
    {
        private readonly IPreUserRepository preUserRepository;

        public PreUserService(IPreUserRepository preUserRepository)
        {
            this.preUserRepository = preUserRepository;
        }

        public PreUser Create(PreUser entity, out string error)
        {
            if (CreateValidation(entity, out error))
            {
                entity = preUserRepository.Create(entity);
            }

            return entity;
        }

        private bool CreateValidation(PreUser entity, out string error)
        {
            error = null;

            if (!IsEmailValid(entity.Email))
            {
                error = ResourceHelper.InvalidEmail();
            }
            else if (preUserRepository.Count(entity.Email) > 0)
            {
                error = ResourceHelper.AlreadyRegisteredEmail();
            }

            return (error == null);
        }

        private bool IsEmailValid(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public IList<PreUser> GetAll()
        {
            return preUserRepository.GetAll();
        }
    }
}
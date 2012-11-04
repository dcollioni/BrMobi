using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BrMobi.ApplicationServices.ServiceInterfaces;
using BrMobi.Core;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Core.Service;

namespace BrMobi.Service.ApplicationServices
{
    public class AccountService : BaseService, IAccountService
    {
        private IUserRepository userRepository;

        public AccountService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IList<User> ListUsers()
        {
            return userRepository.List();
        }

        public User CreateUser(User user, out OperationStatus operationStatus)
        {
            operationStatus = Success();

            IList<string> errors;

            if (ValidateUserCreation(user, out errors))
            {
                user.Password = EncryptPassword(user.Password);
                userRepository.Create(user);
            }
            else
            {
                operationStatus.Success = false;
                operationStatus.Messages = errors;
            }

            return user;
        }

        public bool ExistsEmail(string email)
        {
            return userRepository.CountByEmail(email) > 0;
        }

        public User GetUser(string email, string password)
        {
            password = EncryptPassword(password);
            return userRepository.Get(email, password);
        }

        public User ChangePicture(User user)
        {
            return userRepository.ChangePicture(user);
        }

        /// <summary>
        /// Encrypt the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>The encrypted password.</returns>
        private static string EncryptPassword(string password)
        {
            var passwordBytes = Encoding.Unicode.GetBytes(password);
            var md5Bytes = new MD5Cng().ComputeHash(passwordBytes);
            return Encoding.Unicode.GetString(md5Bytes);
        }

        private bool ValidateUserCreation(User user, out IList<string> errors)
        {
            errors = new List<string>();

            if (ExistsEmail(user.Email))
            {
                errors.Add("Já existe uma conta registrada com esse e-mail.");
            }

            return errors.Count == 0;
        }

        public User GetUser(int id)
        {
            return userRepository.Get(id);
        }

        public User UpdateUser(User user)
        {
            return userRepository.Update(user);
        }
    }
}
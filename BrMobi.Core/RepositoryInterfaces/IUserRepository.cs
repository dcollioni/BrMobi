using System.Collections.Generic;

namespace BrMobi.Core.RepositoryInterfaces
{
    public interface IUserRepository
    {
        IList<User> List();
        User Create(User entity);
        User Update(User entity);
        User ChangePicture(User entity);
        int CountByEmail(string email);

        /// <summary>
        /// Gets an user by email and password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The user.</returns>
        User Get(string email, string password);

        User Get(int id);
        IList<User> GetRelationship(int id);
        void UpdatePassword(string email, string password);
    }
}

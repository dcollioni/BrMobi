using System.Collections.Generic;
using BrMobi.Core;
using BrMobi.Core.Service;

namespace BrMobi.ApplicationServices.ServiceInterfaces
{
    public interface IAccountService
    {
        IList<User> ListUsers();

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user entity to be created.</param>
        /// <param name="operationStatus">The operation status.</param>
        /// <returns>The created user.</returns>
        User CreateUser(User user, out OperationStatus operationStatus);

        /// <summary>
        /// Verify whether the email already exists or not.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>True if the email already exists.</returns>
        bool ExistsEmail(string email);

        /// <summary>
        /// Gets an user by email and password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The user.</returns>
        User GetUser(string email, string password);

        /// <summary>
        /// Updates the user's picture.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The updated user.</returns>
        User ChangePicture(User user);

        /// <summary>
        /// Gets the user by its id.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>The user.</returns>
        User GetUser(int id);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user with data to update.</param>
        /// <returns>The updated user.</returns>
        User UpdateUser(User user);

        /// <summary>
        /// Gets a list of the last users which the user connected to.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>The list of users.</returns>
        IList<User> GetRelationship(int id);
    }
}
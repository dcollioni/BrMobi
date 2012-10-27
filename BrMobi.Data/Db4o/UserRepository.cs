using System;
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public IList<User> List()
        {
            var users = new List<User>();

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    users = client.Query<User>().ToList();
                }
            }

            return users;
        }

        public User Create(User entity)
        {
            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    client.Store(entity);
                }
            }

            return entity;
        }

        public int CountByEmail(string email)
        {
            var count = 0;

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    count = client.Query<User>(u => email.Equals(u.Email, StringComparison.InvariantCultureIgnoreCase)).Count;
                }
            }

            return count;
        }

        public User Get(string email, string password)
        {
            User user = null;

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    user = client.Query<User>(u => u.Email == email && u.Password == password).SingleOrDefault();
                }
            }

            return user;
        }

        public User Update(User entity)
        {
            User user = entity;

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    user = client.Query<User>(u => u.Email == entity.Email).SingleOrDefault();
                    user.Picture = entity.Picture;
                    client.Store(user);
                }
            }

            return user;
        }
    }
}
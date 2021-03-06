﻿using System;
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(Db4objects.Db4o.IObjectServer server)
            : base(server)
        {
        }

        public IList<User> List()
        {
            var users = new List<User>();

            using (var client = Server.OpenClient())
            {
                users = client.Query<User>().ToList();
            }

            return users;
        }

        public User Create(User entity)
        {
            using (var client = Server.OpenClient())
            {
                entity.Id = entity.GetHashCode();
                client.Store(entity);
            }

            return entity;
        }

        public int CountByEmail(string email)
        {
            var count = 0;

            using (var client = Server.OpenClient())
            {
                count = client.Query<User>(u => email.Equals(u.Email, StringComparison.InvariantCultureIgnoreCase)).Count;
            }

            return count;
        }

        public User Get(string email, string password)
        {
            User user = null;

            using (var client = Server.OpenClient())
            {
                user = client.Query<User>(u => u.Email == email && u.Password == password).SingleOrDefault();
            }

            return user;
        }

        public User Update(User entity)
        {
            User user = entity;

            using (var client = Server.OpenClient())
            {
                user = client.Query<User>(u => u.Id == entity.Id).SingleOrDefault();

                if (entity.City != null)
                {
                    entity.City = client.Query<City>(c => c.Id == entity.City.Id).SingleOrDefault();
                }

                user.BirthDate = entity.BirthDate;
                user.City = entity.City;
                user.FacebookLink = entity.FacebookLink;
                user.Gender = entity.Gender;
                user.Name = entity.Name;

                client.Store(user);
            }

            return user;
        }

        public User Get(int id)
        {
            User user = null;

            using (var client = Server.OpenClient())
            {
                user = client.Query<User>(u => u.Id == id).SingleOrDefault();
            }

            return user;
        }

        public User ChangePicture(User entity)
        {
            User user = entity;

            using (var client = Server.OpenClient())
            {
                user = client.Query<User>(u => u.Id == entity.Id).SingleOrDefault();
                user.Picture = entity.Picture;

                client.Store(user);
            }

            return user;
        }

        public IList<User> GetRelationship(int id)
        {
            var users = new List<User>();

            using (var client = Server.OpenClient())
            {
                var messages = client.Query<Message>(m => m.From.Id == id || m.To.Id == id).OrderByDescending(m => m.CreatedOn).ToList();
                var rideOffers = client.Query<RideOfferMarker>(r => r.Owner.Id == id || r.Hitchhikers.Any(h => h.Id == id)).OrderByDescending(m => m.DateTime).ToList();
                var rideRequests = client.Query<RideRequestMarker>(r => r.Owner.Id == id || r.Offers.Any(o => o.Id == id)).OrderByDescending(m => m.DateTime).ToList();
                var answers = client.Query<Answer>(a => (a.CreatedBy.Id == id && a.Marker.Owner.Id != id) || a.Marker.Owner.Id == id && a.CreatedBy.Id != id).OrderByDescending(m => m.CreatedOn).ToList();

                foreach (var message in messages)
                {
                    if (message.From.Id != id)
                    {
                        users.Add(message.From);
                    }
                    else if (message.To.Id != id)
                    {
                        users.Add(message.To);
                    }
                }

                foreach (var rideOffer in rideOffers)
                {
                    if (rideOffer.Owner.Id != id)
                    {
                        users.Add(rideOffer.Owner);
                    }
                    else
                    {
                        users.AddRange(rideOffer.Hitchhikers);
                    }
                }

                foreach (var rideRequest in rideRequests)
                {
                    if (rideRequest.Owner.Id != id)
                    {
                        users.Add(rideRequest.Owner);
                    }
                    else
                    {
                        users.AddRange(rideRequest.Offers);
                    }
                }

                foreach (var answer in answers)
                {
                    if (answer.CreatedBy.Id != id)
                    {
                        users.Add(answer.CreatedBy);
                    }
                    else
                    {
                        users.Add(answer.Marker.Owner);
                    }
                }

                users = users.Distinct().Take(14).ToList();
            }

            return users;
        }

        public void UpdatePassword(string email, string password)
        {
            using (var client = Server.OpenClient())
            {
                var user = client.Query<User>(u => u.Email == email).SingleOrDefault();

                if (user != null)
                {
                    user.Password = password;
                    client.Store(user);
                }
            }
        }
    }
}
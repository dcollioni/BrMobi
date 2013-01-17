using System;
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Map;
using BrMobi.Core.RepositoryInterfaces;

namespace BrMobi.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public User Get(string email, string password)
        {
            User user = null;
            user = Session.Query<User>(u => u.Email == email && u.Password == password).SingleOrDefault();
            return user;
        }

        public User Get(int id)
        {
            User user = null;
            user = Session.Query<User>(u => u.Id == id).SingleOrDefault();
            return user;
        }

        public IList<User> List()
        {
            return Session.Query<User>().ToList();
        }

        public User Create(User entity)
        {
            entity.Id = entity.GetHashCode();
            Session.Store(entity);
            return entity;
        }

        public User Update(User entity)
        {
            User user = entity;
            user = Session.Query<User>(u => u.Id == entity.Id).SingleOrDefault();

            if (entity.City != null)
            {
                entity.City = Session.Query<City>(c => c.Id == entity.City.Id).SingleOrDefault();
            }

            user.BirthDate = entity.BirthDate;
            user.City = entity.City;
            user.FacebookLink = entity.FacebookLink;
            user.Gender = entity.Gender;
            user.Name = entity.Name;

            Session.Store(user);
            return user;
        }

        public int CountByEmail(string email)
        {
            var count = 0;
            count = Session.Query<User>(u => email.Equals(u.Email, StringComparison.InvariantCultureIgnoreCase)).Count;
            return count;
        }

        public User ChangePicture(User entity)
        {
            User user = entity;
            user = Session.Query<User>(u => u.Id == entity.Id).SingleOrDefault();
            user.Picture = entity.Picture;
            Session.Store(user);
            return user;
        }

        public IList<User> GetRelationship(int id)
        {
            var users = new List<User>();

            var messages = Session.Query<Message>(m => m.From.Id == id || m.To.Id == id).OrderByDescending(m => m.CreatedOn).ToList();
            var rideOffers = Session.Query<RideOfferMarker>(r => r.Owner.Id == id || r.Hitchhikers.Any(h => h.Id == id)).OrderByDescending(m => m.DateTime).ToList();
            var rideRequests = Session.Query<RideRequestMarker>(r => r.Owner.Id == id || r.Offers.Any(o => o.Id == id)).OrderByDescending(m => m.DateTime).ToList();
            var answers = Session.Query<Answer>(a => (a.CreatedBy.Id == id && a.Marker.Owner.Id != id) || a.Marker.Owner.Id == id && a.CreatedBy.Id != id).OrderByDescending(m => m.CreatedOn).ToList();

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
            return users;
        }

        public void UpdatePassword(string email, string password)
        {
            var user = Session.Query<User>(u => u.Email == email).SingleOrDefault();

            if (user != null)
            {
                user.Password = password;
                Session.Store(user);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Map;
using BrMobi.Core.RepositoryInterfaces.Map;

namespace BrMobi.Data.Repositories.Map
{
    public class HelpMarkerRepository : BaseRepository, IHelpMarkerRepository
    {
        public void Create(HelpMarker helpMarker)
        {
            helpMarker.Owner = Session.Query<User>(u => u.Email == helpMarker.Owner.Email).SingleOrDefault();
            helpMarker.Id = helpMarker.GetHashCode();
            helpMarker.CreatedOn = DateTime.Now;
            Session.Store(helpMarker);
        }

        public IList<HelpMarker> List(LatLng southWest, LatLng northEast, User loggedUser)
        {
            return Session.Query<HelpMarker>(m => 
                southWest.Lat() <= m.Lat && m.Lat <= northEast.Lat() && southWest.Lng() <= m.Lng && m.Lng <= northEast.Lng() 
                    && (m.Owner.Email == loggedUser.Email || !string.IsNullOrEmpty(m.Question))).ToList();
        }

        public HelpMarker UpdateQuestion(int markerId, string question)
        {
            var marker = Session.Query<HelpMarker>(m => m.Id == markerId).SingleOrDefault();

            if (marker != null)
            {
                marker.Question = question;
                Session.Store(marker);
            }
            return marker;
        }

        public HelpMarker Get(int id)
        {
            return Session.Query<HelpMarker>(m => m.Id == id).SingleOrDefault();
        }

        public Answer AddAnswer(Answer answer, int markerId)
        {
            answer.Id = answer.GetHashCode();
            answer.CreatedBy = Session.Query<User>(u => u.Id == answer.CreatedBy.Id).SingleOrDefault();
            answer.Marker = Session.Query<HelpMarker>(m => m.Id == markerId).SingleOrDefault();
            Session.Store(answer);
            return answer;
        }

        public void RemoveAnswer(int answerId)
        {
            var answer = Session.Query<Answer>(a => a.Id == answerId).SingleOrDefault();
            if (answer != null)
            {
                Session.Delete(answer);
            }
        }

        public void Remove(int markerId)
        {
            var marker = Session.Query<HelpMarker>(m => m.Id == markerId).SingleOrDefault();
            Session.Delete(marker);

            var answers = Session.Query<Answer>(a => a.Marker.Id == markerId);
            answers.ToList().ForEach(a => Session.Delete(a));
        }
    }
}
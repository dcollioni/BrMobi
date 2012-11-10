﻿using System;
using System.Collections.Generic;
using System.Linq;
using BrMobi.Core;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces.Map;
using BrMobi.Data.Db4o.Base;

namespace BrMobi.Data.Db4o.Map
{
    public class HelpMarkerRepository : BaseRepository, IHelpMarkerRepository
    {
        public void Create(HelpMarker helpMarker)
        {
            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    helpMarker.Owner = client.Query<User>(u => u.Email == helpMarker.Owner.Email).SingleOrDefault();
                    helpMarker.Id = helpMarker.GetHashCode();
                    helpMarker.CreatedOn = DateTime.Now;
                    client.Store(helpMarker);
                }
            }
        }

        public IList<HelpMarker> List(LatLng southWest, LatLng northEast, User loggedUser)
        {
            var markers = new List<HelpMarker>();

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    markers = client.Query<HelpMarker>(m => southWest.Lat() <= m.Lat && m.Lat <= northEast.Lat() &&
                                                            southWest.Lng() <= m.Lng && m.Lng <= northEast.Lng() &&
                                                            (
                                                                (m.Owner.Email == loggedUser.Email) ||
                                                                (!string.IsNullOrEmpty(m.Question))
                                                            )).ToList();
                }
            }

            return markers;
        }

        public HelpMarker UpdateQuestion(int markerId, string question)
        {
            HelpMarker marker = null;

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    marker = client.Query<HelpMarker>(m => m.Id == markerId).SingleOrDefault();

                    if (marker != null)
                    {
                        marker.Question = question;
                        client.Store(marker);
                    }
                }
            }

            return marker;
        }

        public HelpMarker Get(int id)
        {
            HelpMarker marker = null;

            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    marker = client.Query<HelpMarker>(m => m.Id == id).SingleOrDefault();
                }
            }

            return marker;
        }

        public Answer AddAnswer(Answer answer, int markerId)
        {
            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    answer.Id = answer.GetHashCode();
                    answer.CreatedBy = client.Query<User>(u => u.Id == answer.CreatedBy.Id).SingleOrDefault();
                    answer.Marker = client.Query<HelpMarker>(m => m.Id == markerId).SingleOrDefault();
                    client.Store(answer);
                }
            }

            return answer;
        }

        public void RemoveAnswer(int answerId)
        {
            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    var answer = client.Query<Answer>(a => a.Id == answerId).SingleOrDefault();

                    if (answer != null)
                    {
                        client.Delete(answer);
                    }
                }
            }
        }

        public void Remove(int markerId)
        {
            using (var server = Server)
            {
                using (var client = server.OpenClient())
                {
                    var marker = client.Query<HelpMarker>(m => m.Id == markerId).SingleOrDefault();
                    client.Delete(marker);
                }
            }
        }
    }
}
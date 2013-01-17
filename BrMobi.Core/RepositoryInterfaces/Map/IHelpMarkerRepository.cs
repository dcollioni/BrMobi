using System.Collections.Generic;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Map;

namespace BrMobi.Core.RepositoryInterfaces.Map
{
    public interface IHelpMarkerRepository
    {
        void Create(HelpMarker helpMarker);
        IList<HelpMarker> List(LatLng southWest, LatLng northEast, User loggedUser);
        HelpMarker UpdateQuestion(int markerId, string question);
        HelpMarker Get(int id);
        Answer AddAnswer(Answer answer, int markerId);
        void RemoveAnswer(int answerId);
        void Remove(int markerId);
    }
}
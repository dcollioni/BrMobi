using System.Collections.Generic;
using BrMobi.Core.Map;

namespace BrMobi.Core.RepositoryInterfaces.Map
{
    public interface IHelpMarkerRepository
    {
        void Create(HelpMarker helpMarker);
        IList<HelpMarker> List(LatLng southWest, LatLng northEast);
    }
}
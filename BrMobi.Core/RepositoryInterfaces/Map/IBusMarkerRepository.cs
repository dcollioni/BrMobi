using System.Collections.Generic;
using BrMobi.Core.Map;

namespace BrMobi.Core.RepositoryInterfaces.Map
{
    public interface IBusMarkerRepository
    {
        void Create(BusMarker busMarker);
        IList<BusMarker> List(LatLng southWest, LatLng northEast);
        BusMarker AddLine(int busMarkerId, int busLineId);
        BusMarker Get(int id);
        IList<BusLine> ListBusLines(int busMarkerId);
        void RemoveBusLine(int busLineId, int markerId);
        void Remove(int markerId);
    }
}
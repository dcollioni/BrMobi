using System.Collections.Generic;
using BrMobi.Core.Entities.Map;

namespace BrMobi.Core.RepositoryInterfaces
{
    public interface IMarkerRepository
    {
        IList<Marker> List(int ownerId, int page);
        void DeleteAll(int ownerId);
    }
}

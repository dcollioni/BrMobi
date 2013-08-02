using System.Collections.Generic;
using BrMobi.Core.ViewModels.Map;

namespace BrMobi.ApplicationServices.ServiceInterfaces.Map
{
    public interface IMarkerService
    {
        List<RideViewModel> List(int ownerId, int page);
        void DeleteAll(int ownerId);
    }
}
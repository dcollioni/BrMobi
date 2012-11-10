using BrMobi.Core;
using BrMobi.Core.Map;

namespace BrMobi.ApplicationServices.ServiceInterfaces.Map
{
    public interface IBusLineService
    {
        BusLine Create(BusLine busLine, int busMarkerId);
    }
}
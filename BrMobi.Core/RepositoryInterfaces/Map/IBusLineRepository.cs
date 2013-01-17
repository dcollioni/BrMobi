using BrMobi.Core.Entities.Map;

namespace BrMobi.Core.RepositoryInterfaces.Map
{
    public interface IBusLineRepository
    {
        BusLine Get(string name);
        BusLine Create(BusLine busLine);
    }
}
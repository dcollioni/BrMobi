using BrMobi.ApplicationServices.ServiceInterfaces.Map;
using BrMobi.Core.Map;
using BrMobi.Core.RepositoryInterfaces.Map;

namespace BrMobi.ApplicationServices.Map
{
    public class BusLineService : IBusLineService
    {
        private readonly IBusLineRepository busLineRepository;
        private readonly IBusMarkerRepository busMarkerRepository;

        public BusLineService(IBusLineRepository busLineRepository,
                              IBusMarkerRepository busMarkerRepository)
        {
            this.busLineRepository = busLineRepository;
            this.busMarkerRepository = busMarkerRepository;
        }

        public BusLine Create(BusLine busLine, int busMarkerId)
        {
            var storedBusLine = busLineRepository.Get(busLine.Name);

            if (storedBusLine == null)
            {
                storedBusLine = busLineRepository.Create(busLine);
            }

            busMarkerRepository.AddLine(busMarkerId, storedBusLine.Id);

            return storedBusLine;
        }
    }
}
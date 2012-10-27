using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrMobi.ApplicationServices.ServiceInterfaces.Map;
using BrMobi.Core.RepositoryInterfaces.Map;
using BrMobi.Core.Extensions;

namespace BrMobi.ApplicationServices.Map
{
    public class BusMarkerService : IBusMarkerService
    {
        private readonly IBusMarkerRepository busMarkerRepository;

        public BusMarkerService(IBusMarkerRepository busMarkerRepository)
        {
            this.busMarkerRepository = busMarkerRepository;
        }

        public string GetBusMarkerInfoDetails(int id, string template)
        {
            var busLines = busMarkerRepository.ListBusLines(id);

            StringBuilder details = new StringBuilder("<li class='noLines'>Não há linhas associadas a este ponto de ônibus.</li>");

            if (busLines.Count > 0)
            {
                details.Clear();

                foreach (var busLine in busLines)
                {
                    details.AppendFormat("<li><a href='{0}' target='_blank'>{1}</a></li>", busLine.InfoUrl, busLine.Name);
                }
            }

            return template.SetParam("details", details.ToString());
        }
    }
}
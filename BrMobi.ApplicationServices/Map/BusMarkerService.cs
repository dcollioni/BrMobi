using System.Text;
using BrMobi.ApplicationServices.ServiceInterfaces.Map;
using BrMobi.Core;
using BrMobi.Core.Extensions;
using BrMobi.Core.RepositoryInterfaces.Map;

namespace BrMobi.ApplicationServices.Map
{
    public class BusMarkerService : IBusMarkerService
    {
        private readonly IBusMarkerRepository busMarkerRepository;

        public BusMarkerService(IBusMarkerRepository busMarkerRepository)
        {
            this.busMarkerRepository = busMarkerRepository;
        }

        public string GetBusMarkerInfoDetails(int id, string template, User loggedUser)
        {
            var marker = busMarkerRepository.Get(id);

            StringBuilder details = new StringBuilder("<li class='noLines'>Não há linhas associadas a este ponto de ônibus.</li>");

            if (marker.Lines.Count > 0)
            {
                details.Clear();

                foreach (var busLine in marker.Lines)
                {
                    var remove = marker.Owner.Id == loggedUser.Id ? "<input type='button' class='remove' value='Excluir'>" : "";

                    details.AppendFormat("<li><input type='hidden' name='busLineId' value='{3}' /><a href='{0}' target='_blank'>{1}</a>{2}</li>", busLine.InfoUrl, busLine.Name, remove, busLine.Id);
                }
            }

            var displayOwner = marker.Owner.Id == loggedUser.Id ? "inline" : "none";

            return template.SetParam("details", details.ToString())
                           .SetParam("displayOwner", displayOwner);
        }

        public void RemoveBusLine(int busLineId, int markerId, User loggedUser)
        {
            var marker = busMarkerRepository.Get(markerId);

            if (marker.Owner.Id == loggedUser.Id)
            {
                busMarkerRepository.RemoveBusLine(busLineId, markerId);
            }
        }

        public void Remove(int markerId, User loggedUser)
        {
            var marker = busMarkerRepository.Get(markerId);

            if (marker.Owner.Id == loggedUser.Id)
            {
                busMarkerRepository.Remove(markerId);
            }
        }
    }
}
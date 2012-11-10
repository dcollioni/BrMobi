using BrMobi.Core;

namespace BrMobi.ApplicationServices.ServiceInterfaces.Map
{
    public interface IBusMarkerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The bus marker id.</param>
        /// <param name="template">The info template.</param>
        /// <returns></returns>
        string GetBusMarkerInfoDetails(int id, string template, User loggedUser);

        /// <summary>
        /// Removes the bus line from the marker.
        /// </summary>
        /// <param name="busLineId">The bus line id.</param>
        /// <param name="markerId">The marker id.</param>
        /// <param name="loggedUser">The logged user.</param>
        void RemoveBusLine(int busLineId, int markerId, User loggedUser);

        /// <summary>
        /// Remover a bus marker.
        /// </summary>
        /// <param name="markerId">The bus marker id.</param>
        /// <param name="loggedUser">The logged user.</param>
        void Remove(int markerId, User loggedUser);
    }
}
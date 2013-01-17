using System;
using BrMobi.Core;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Map;

namespace BrMobi.ApplicationServices.ServiceInterfaces.Map
{
    public interface IRideOfferMarkerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The ride offer marker id.</param>
        /// <param name="template">The info template.</param>
        /// <param name="loggedUser">The user logged.</param>
        /// <returns></returns>
        string GetRideOfferMarkerInfoDetails(int id, string template, User loggedUser);

        /// <summary>
        /// Updates the ride offer marker.
        /// </summary>
        /// <param name="dateTime">The date time to update the marker with.</param>
        /// <param name="destination">The destination to update the marker with.</param>
        /// <param name="id">The marker id.</param>
        /// <param name="loggedUser">The user logged.</param>
        /// <returns>The updated marker.</returns>
        RideOfferMarker Update(DateTime dateTime, string destination, int id, User loggedUser);

        /// <summary>
        /// Adds a hitchhiker to the ride offer.
        /// </summary>
        /// <param name="rideOfferId">The ride offer id.</param>
        /// <param name="hitchhikerId">The hitchhiker (a user) id.</param>
        /// <returns>The updated ride offer marker.</returns>
        RideOfferMarker AddHitchhiker(int rideOfferId, int hitchhikerId);

        /// <summary>
        /// Removes a hitchhiker from the ride offer.
        /// </summary>
        /// <param name="rideOfferId">The ride offer id.</param>
        /// <param name="hitchhikerId">The hitchhiker (a user) id.</param>
        /// <returns>The updated ride offer marker.</returns>
        RideOfferMarker RemoveHitchhiker(int rideOfferId, int hitchhikerId);

        /// <summary>
        /// Removes the marker.
        /// </summary>
        /// <param name="markerId">The marker id.</param>
        /// <param name="loggedUser">The logged user.</param>
        void Remove(int markerId, User loggedUser);
    }
}
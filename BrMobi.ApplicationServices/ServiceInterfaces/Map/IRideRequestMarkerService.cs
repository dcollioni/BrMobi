using System;
using System.Collections.Generic;
using BrMobi.Core;
using BrMobi.Core.Entities;
using BrMobi.Core.Entities.Map;

namespace BrMobi.ApplicationServices.ServiceInterfaces.Map
{
    public interface IRideRequestMarkerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The ride request marker id.</param>
        /// <param name="template">The info template.</param>
        /// <param name="loggedUser">The user logged.</param>
        /// <returns></returns>
        string GetRideRequestMarkerInfoDetails(int id, string template, User loggedUser);

        /// <summary>
        /// Updates the ride request marker.
        /// </summary>
        /// <param name="dateTime">The date time to update the marker with.</param>
        /// <param name="destination">The destination to update the marker with.</param>
        /// <param name="id">The marker id.</param>
        /// <param name="loggedUser">The user logged.</param>
        /// <returns>The updated marker.</returns>
        RideRequestMarker Update(DateTime dateTime, string destination, int id, User loggedUser);

        /// <summary>
        /// Adds an offer to the ride request.
        /// </summary>
        /// <param name="rideRequestId">The ride request id.</param>
        /// <param name="offerId">The offer (a user) id.</param>
        /// <returns>The updated ride request marker.</returns>
        RideRequestMarker AddOffer(int rideRequestId, int offerId);

        /// <summary>
        /// Removes an offer from the ride request.
        /// </summary>
        /// <param name="rideRequestId">The ride request id.</param>
        /// <param name="offerId">The offer (a user) id.</param>
        /// <returns>The updated ride request marker.</returns>
        RideRequestMarker RemoveOffer(int rideRequestId, int offerId);

        /// <summary>
        /// Removes the marker.
        /// </summary>
        /// <param name="markerId">The marker id.</param>
        /// <param name="loggedUser">The logged user.</param>
        void Remove(int markerId, User loggedUser);
    }
}
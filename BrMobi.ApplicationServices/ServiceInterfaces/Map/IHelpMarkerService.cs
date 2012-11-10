using System.Collections.Generic;
using BrMobi.Core;
using BrMobi.Core.Map;

namespace BrMobi.ApplicationServices.ServiceInterfaces.Map
{
    public interface IHelpMarkerService
    {
        /// <summary>
        ///  Updates marker.
        /// </summary>
        /// <param name="markerId">The marker id.</param>
        /// <param name="question">The question to update the marker with.</param>
        /// <returns>The updated marker</returns>
        HelpMarker UpdateQuestion(int markerId, string question);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The marker id.</param>
        /// <param name="template">The info template.</param>
        /// <param name="loggedUser">The user logged.</param>
        /// <returns></returns>
        string GetHelpMarkerInfoDetails(int id, string template, User loggedUser);

        /// <summary>
        /// Adds an answer to the marker.
        /// </summary>
        /// <param name="answer">The answer.</param>
        /// <param name="markerId">The marker id.</param>
        /// <returns>The answer.</returns>
        Answer AddAnswer(Answer answer, int markerId);

        /// <summary>
        /// Removes an answer from help marker.
        /// </summary>
        /// <param name="answerId">The answer id.</param>
        /// <param name="loggedUser">The logged user.</param>
        void RemoveAnswer(int answerId, User loggedUser);

        /// <summary>
        /// Lists the answers of a marker.
        /// </summary>
        /// <param name="markerId">The marker id.</param>
        /// <returns>The answers.</returns>
        IList<Answer> ListAnswers(int markerId);

        /// <summary>
        /// Removes the marker.
        /// </summary>
        /// <param name="markerId">The marker id.</param>
        /// <param name="loggedUser">The logged user.</param>
        void Remove(int markerId, User loggedUser);
    }
}
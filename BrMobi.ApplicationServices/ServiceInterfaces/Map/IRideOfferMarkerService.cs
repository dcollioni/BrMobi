namespace BrMobi.ApplicationServices.ServiceInterfaces.Map
{
    public interface IRideOfferMarkerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The ride offer marker id.</param>
        /// <param name="template">The info template.</param>
        /// <returns></returns>
        string GetRideOfferMarkerInfoDetails(int id, string template);
    }
}
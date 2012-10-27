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
        string GetBusMarkerInfoDetails(int id, string template);
    }
}
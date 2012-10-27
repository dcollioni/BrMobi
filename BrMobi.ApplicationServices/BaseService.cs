using BrMobi.Core.Service;

namespace BrMobi.Service.ApplicationServices
{
    public class BaseService
    {
        protected OperationStatus Success()
        {
            return new OperationStatus(true);
        }
    }
}

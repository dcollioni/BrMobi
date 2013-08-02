using BrMobi.Core.Entities;

namespace BrMobi.ApplicationServices.ServiceInterfaces
{
    public interface IPreUserService
    {
        PreUser Create(PreUser entity, out string error);
    }
}
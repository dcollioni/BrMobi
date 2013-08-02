using BrMobi.Core.Entities;
using BrMobi.Core.RepositoryInterfaces;

namespace BrMobi.Data.Repositories
{
    public class PreUserRepository : BaseRepository, IPreUserRepository
    {
        public PreUser Create(PreUser entity)
        {
            entity.Id = entity.GetHashCode();
            Session.Store(entity);
            return entity;
        }

        public int Count(string email)
        {
            return Session.Query<PreUser>(p => p.Email == email).Count;
        }
    }
}
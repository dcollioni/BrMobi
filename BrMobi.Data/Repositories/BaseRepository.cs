using Db4objects.Db4o;

namespace BrMobi.Data.Repositories
{
    public class BaseRepository
    {
        public IObjectContainer Session
        {
            get { return Server.Session; }
        }
    }
}
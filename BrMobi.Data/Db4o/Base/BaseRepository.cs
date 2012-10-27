using System;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;

namespace BrMobi.Data.Db4o.Base
{
    public class BaseRepository
    {
        static string YapFileName
        {
            get
            {
                var folder = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
                var yapFile = string.Format("{0}/{1}", folder, "BrMobiObjects.yap");

                return yapFile;
            }
        }

        IServerConfiguration ServerConfig
        {
            get
            {
                return Db4oClientServer.NewServerConfiguration();
            }
        }

        protected IObjectServer Server
        {
            get
            {
                return Db4oClientServer.OpenServer(ServerConfig, YapFileName, 0);
            }
        }
    }
}
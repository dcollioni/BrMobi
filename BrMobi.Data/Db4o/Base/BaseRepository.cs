﻿using System;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;

namespace BrMobi.Data.Db4o.Base
{
    public class BaseRepository
    {
        //protected static string YapFileName
        //{
        //    get
        //    {
        //        var folder = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
        //        var yapFile = string.Format("{0}/{1}", folder, "BrMobiObjects.yap");

        //        return yapFile;
        //    }
        //}

        //IServerConfiguration ServerConfig
        //{
        //    get
        //    {
        //        return Db4oClientServer.NewServerConfiguration();
        //    }
        //}

        ////IObjectServer server;
        ////protected IObjectServer Server
        ////{
        ////    get
        ////    {
        ////        server = Db4oClientServer.OpenServer(YapFileName, 60001);
        ////        server.GrantAccess("db4o", "passwordOfUser");

        ////        //Db4oEmbedded.OpenFile(Db4oEmbedded.NewConfiguration(), YapFileName);

        ////        //if (server == null)
        ////        //{
        ////        //    server = Db4oClientServer.OpenServer(YapFileName, 60001);
        ////        //}

        ////        return server;
        ////    }
        ////}

        //protected IObjectContainer Client
        //{
        //    get
        //    {
        //        //var server = Server;
        //        return Db4oClientServer.OpenClient("localhost", 4000, "db4o", "db4o");
        //        //return Db4oClientServer.OpenClient("brmobi.apphb.com", 0, "db4o", "db4o");
        //    }
        //}
    }
}
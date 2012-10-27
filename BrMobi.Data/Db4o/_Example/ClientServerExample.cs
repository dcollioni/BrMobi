using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;

namespace BrMobi.Repository.Db4o._Example
{
    public class ClientServerExample
    {
        readonly static string YapFileName = Path.GetFullPath("~/App_Data/BrMobiObjects.yap");
        readonly static int ServerPort = 4488;
        readonly static string ServerUser = "db4o";
        readonly static string ServerPassword = "db4o";
        public static void Main(string[] args)
        {
            IServerConfiguration config = Db4oClientServer.NewServerConfiguration();

            using (IObjectServer server = Db4oClientServer.OpenServer(config, YapFileName, 0))
            {
                using (IObjectContainer client = server.OpenClient())
                {
                    var pass = "teste";

                    var user = new Core.User()
                    {
                        CreatedOn = DateTime.Now,
                        Email = "teste@teste.com",
                        Name = "Teste 1",
                        Password = Encoding.Unicode.GetString(new MD5CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(pass)))
                    };

                    client.Store(user);
                }
            }

            //AccessRemoteServer();
            //using (IObjectServer server = Db4oClientServer.OpenServer(Db4oClientServer.NewServerConfiguration(),
            //    YapFileName, ServerPort))
            //{
            //    server.GrantAccess(ServerUser, ServerPassword);
            //    QueryRemoteServer(ServerPort, ServerUser, ServerPassword);
            //    DemonstrateRemoteReadCommitted(ServerPort, ServerUser, ServerPassword);
            //    DemonstrateRemoteRollback(ServerPort, ServerUser, ServerPassword);
            //}
        }

        public static void QueryLocalServer(IObjectServer server)
        {
            //using (IObjectContainer client = server.OpenClient())
            //{
            //    ListResult(client.QueryByExample(new Car(null)));
            //}
        }

        public static void DemonstrateLocalReadCommitted(IObjectServer server)
        {
            //using (IObjectContainer client1 = server.OpenClient(),
            //    client2 = server.OpenClient())
            //{
            //    Pilot pilot = new Pilot("David Coulthard", 98);
            //    IObjectSet result = client1.QueryByExample(new Car("BMW"));
            //    Car car = (Car)result.Next();
            //    car.Pilot = pilot;
            //    client1.Store(car);
            //    ListResult(client1.QueryByExample(new Car(null)));
            //    ListResult(client2.QueryByExample(new Car(null)));
            //    client1.Commit();
            //    ListResult(client1.QueryByExample(typeof(Car)));
            //    ListRefreshedResult(client2, client2.QueryByExample(typeof(Car)), 2);
            //}
        }

        public static void DemonstrateLocalRollback(IObjectServer server)
        {
            //using (IObjectContainer client1 = server.OpenClient(),
            //    client2 = server.OpenClient())
            //{
            //    IObjectSet result = client1.QueryByExample(new Car("BMW"));
            //    Car car = (Car)result.Next();
            //    car.Pilot = new Pilot("Someone else", 0);
            //    client1.Store(car);
            //    ListResult(client1.QueryByExample(new Car(null)));
            //    ListResult(client2.QueryByExample(new Car(null)));
            //    client1.Rollback();
            //    client1.Ext().Refresh(car, 2);
            //    ListResult(client1.QueryByExample(new Car(null)));
            //    ListResult(client2.QueryByExample(new Car(null)));
            //}
        }

        public static void AccessRemoteServer()
        {
            using (IObjectServer server = Db4oClientServer.OpenServer(YapFileName, ServerPort))
            {
                server.GrantAccess(ServerUser, ServerPassword);
                using (IObjectContainer client = Db4oClientServer.OpenClient("localhost", ServerPort, ServerUser, ServerPassword))
                {
                    // Do something with this client, or open more clients
                }
            }
        }

        public static void QueryRemoteServer(int port, string user, string password)
        {
            //using (IObjectContainer client = Db4oClientServer.OpenClient("localhost", port, user, password))
            //{
            //    ListResult(client.QueryByExample(new Car(null)));
            //}
        }

        public static void DemonstrateRemoteReadCommitted(int port, string user, string password)
        {
            //using (IObjectContainer client1 = Db4oClientServer.OpenClient("localhost", port, user, password),
            //        client2 = Db4oClientServer.OpenClient("localhost", port, user, password))
            //{
            //    Pilot pilot = new Pilot("Jenson Button", 97);
            //    IObjectSet result = client1.QueryByExample(new Car(null));
            //    Car car = (Car)result.Next();
            //    car.Pilot = pilot;
            //    client1.Store(car);
            //    ListResult(client1.QueryByExample(new Car(null)));
            //    ListResult(client2.QueryByExample(new Car(null)));
            //    client1.Commit();
            //    ListResult(client1.QueryByExample(new Car(null)));
            //    ListResult(client2.QueryByExample(new Car(null)));
            //}
        }

        public static void DemonstrateRemoteRollback(int port, string user, string password)
        {
            //using (IObjectContainer client1 = Db4oClientServer.OpenClient("localhost", port, user, password),
            //    client2 = Db4oClientServer.OpenClient("localhost", port, user, password))
            //{
            //    IObjectSet result = client1.QueryByExample(new Car(null));
            //    Car car = (Car)result.Next();
            //    car.Pilot = new Pilot("Someone else", 0);
            //    client1.Store(car);
            //    ListResult(client1.QueryByExample(new Car(null)));
            //    ListResult(client2.QueryByExample(new Car(null)));
            //    client1.Rollback();
            //    client1.Ext().Refresh(car, 2);
            //    ListResult(client1.QueryByExample(new Car(null)));
            //    ListResult(client2.QueryByExample(new Car(null)));
            //}
        }
    }
}
﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using BrMobi.Web.CastleWindsor;
using BrMobi.Web.Controllers;
using BrMobi.Web.CustomModelBinders;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using SharpArch.Web.Castle;
using Db4objects.Db4o.CS;
using Db4objects.Db4o;
using System.Web.Configuration;
using System.Web.Services.Description;
using System.Net.Sockets;

namespace BrMobi.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static IObjectServer Db4oServer;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Access", "Acesso", new { controller = "Account", action = "Access" });
            routes.MapRoute("Logon", "Entrar", new { controller = "Account", action = "LogOn" });
            routes.MapRoute("Logoff", "Sair", new { controller = "Account", action = "LogOff" });
            routes.MapRoute("Register", "Cadastro", new { controller = "Account", action = "Register" });
            routes.MapRoute("Profile", "Perfil/{id}", new { controller = "Profile", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("Evaluation", "Avaliacao", new { controller = "Evaluation", action = "Index" });
            routes.MapRoute("EvaluationResult", "ResultadoAvaliacao", new { controller = "Evaluation", action = "Result" });
            routes.MapRoute("ResetPassword", "RedefinirSenha/{success}", new { controller = "Account", action = "ResetPassword", success = UrlParameter.Optional });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            InitializeDb4oServer();

            this.InitializeServiceLocator();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime), new DateModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new DateModelBinder());
            ModelBinders.Binders.Add(typeof(TimeSpan), new TimeModelBinder());
        }

        protected void Application_End()
        {
            if (Db4oServer != null)
            {
                Db4oServer.Close();
                Db4oServer.Dispose();
            }
        }

        //protected void Application_Disposed()
        //{
        //    if (db4oServer != null)
        //    {
        //        db4oServer.Close();
        //        db4oServer.Dispose();
        //    }
        //}

        //protected void Session_End()
        //{
        //    if (db4oServer != null)
        //    {
        //        db4oServer.Close();
        //        db4oServer.Dispose();
        //    }
        //}

        private static void InitializeDb4oServer()
        {
            var folder = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var yapFile = string.Format("{0}/{1}", folder, "BrMobiObjects.yap");

            Db4oClientServer.NewServerConfiguration();
            Db4oServer = Db4oClientServer.OpenServer(Db4oClientServer.NewServerConfiguration(), yapFile, 0);
                //db4oServer.GrantAccess("db4o", "db4o");
        }

        public static IObjectContainer OpenClient()
        {
            return Db4oServer.OpenClient();
        }

        /// <summary>
        /// Instantiate the container and add all Controllers that derive from
        /// WindsorController to the container.  Also associate the Controller
        /// with the WindsorContainer ControllerFactory.
        /// </summary>
        protected virtual void InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            container.RegisterControllers(typeof(HomeController).Assembly);
            ComponentRegistrar.AddComponentsTo(container);

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}
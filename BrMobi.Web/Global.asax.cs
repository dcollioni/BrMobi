using System;
using System.Web.Mvc;
using System.Web.Routing;
using BrMobi.Web.CastleWindsor;
using BrMobi.Web.Controllers;
using BrMobi.Web.CustomModelBinders;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Microsoft.Practices.ServiceLocation;
using SharpArch.Web.Castle;
using BrMobi.Web.Attributes;
using System.Web;
using System.Web.Security;

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

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime), new DateModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new DateModelBinder());
            ModelBinders.Binders.Add(typeof(TimeSpan), new TimeModelBinder());

            InitializeDb4oServer();
            this.InitializeServiceLocator();
        }

        protected void Application_End()
        {
            if (Db4oServer != null)
            {
                Db4oServer.Close();
            }
        }

        protected virtual void InitializeDb4oServer()
        {
            var folder = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var yapFile = string.Format("{0}\\{1}", folder, "BrMobiObjects.yap");

            Db4oServer = Db4oClientServer.OpenServer(yapFile, 0);
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

        protected void Application_EndRequest()
        {
            var context = new HttpContextWrapper(this.Context);

            // If we're an ajax request and forms authentication caused a 302, 
            // then we actually need to do a 401
            if (FormsAuthentication.IsEnabled && context.Response.StatusCode == 302
                && context.Request.IsAjaxRequest() && Session["User"] == null)
            {
                context.Response.Clear();
                context.Response.StatusCode = 401;
            }
        }
    }
}
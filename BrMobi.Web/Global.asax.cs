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
using System.Web;
using System.Web.Security;

namespace BrMobi.Web
{
    public class MvcApplication : HttpApplication
    {
        public static IObjectServer Db4OServer;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Welcome", "BemVindo", new { controller = "Account", action = "Welcome" });
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

            InitializeDb4OServer();
            this.InitializeServiceLocator();
        }

        protected void Application_End()
        {
            if (Db4OServer != null)
            {
                Db4OServer.Close();
            }
        }

        protected virtual void InitializeDb4OServer()
        {
            var folder = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var yapFile = string.Format("{0}\\{1}", folder, "BrMobiObjects.yap");
            Db4OServer = Db4oClientServer.OpenServer(yapFile, 0);
        }

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
            var context = new HttpContextWrapper(Context);
            if (FormsAuthentication.IsEnabled && context.Response.StatusCode == 302 && context.Request.IsAjaxRequest())
            {
                context.Response.Clear();
                context.Response.StatusCode = 401;
            }
        }
    }
}